
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace VaultConfigLib;

public class VaultConfigurationProvider(IVaultClient vaultClient, IConfiguration configuration, string secretPath, string mountPoint = "secret")
    : ConfigurationProvider
{
    public override void Load()
    {
        LoadSecretsAsync().GetAwaiter().GetResult();
    }

    private async Task LoadSecretsAsync()
    {
        var secretKey = configuration["Vault:MapToKey"];
        var secret = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretPath, mountPoint: mountPoint);
        secret.Data.Data.TryGetValue("value", out var secretValue);
        if (secretValue != null)
        {
            Data[secretKey] = secretValue.ToString();
        }
    }
}