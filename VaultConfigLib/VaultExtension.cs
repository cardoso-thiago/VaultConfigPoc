using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaultSharp;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;

namespace VaultConfigLib;

public static class VaultExtension
{
    public static IServiceCollection AddVaultClient(this IServiceCollection services, string vaultUrl, string rootToken)
    {
        if (string.IsNullOrEmpty(vaultUrl))
            throw new ArgumentException("A URL do Vault não pode ser nula ou vazia.", nameof(vaultUrl));

        if (string.IsNullOrEmpty(rootToken))
            throw new ArgumentException("O token não pode ser nulo ou vazio.", nameof(rootToken));

        IAuthMethodInfo authMethod = new TokenAuthMethodInfo(rootToken);

        var vaultClientSettings = new VaultClientSettings(vaultUrl, authMethod);
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);

        services.AddSingleton(vaultClient);
        return services;
    }
    
    //TODO validar, pode ser dependente da configuração dinâmica, o Vault:SecretPath pode estar armazenado externamente
    //Qual seria o comportamento nesse caso?
    public static IConfigurationBuilder AddVaultSecrets(this IConfigurationBuilder builder, IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var vaultClient = serviceProvider.GetRequiredService<IVaultClient>();
        
        var secretPath = configuration["Vault:SecretPath"];
        var provider = new VaultConfigurationProvider(vaultClient, configuration, secretPath);
        builder.Add(new VaultConfigurationSource(provider));
        return builder;
    }
}