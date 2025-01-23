using Microsoft.Extensions.Configuration;

namespace VaultConfigLib;

public class VaultConfigurationSource(VaultConfigurationProvider vaultConfigurationProvider) : IConfigurationSource
{
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return vaultConfigurationProvider;
    }
}