namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public interface IAzureKeyVaultService
{
    string GetSecret(string secretName);
}