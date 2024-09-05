using Azure.Security.KeyVault.Secrets;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class AzureKeyVaultService(IConfiguration configuration) : IAzureKeyVaultService
{
    public string GetSecret(string secretName)
    {
        ArgumentNullException.ThrowIfNull(secretName);
        return configuration[secretName] ?? string.Empty;
    }
}