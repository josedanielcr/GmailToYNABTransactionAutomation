using Azure.Identity;

namespace Gmail_To_YNAB_Transaction_Automation_API.Configuration;

public static class ExternalServices
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddAzureKeyVault( 
            new Uri($"https://{configuration["KeyVaultName"]}.vault.azure.net/"),
            new DefaultAzureCredential());
        return services;
    }
}