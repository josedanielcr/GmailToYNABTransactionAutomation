using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;
using Gmail_To_YNAB_Transaction_Automation_API.Services;

namespace Gmail_To_YNAB_Transaction_Automation_API.Configuration;

public static class InternalServices
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<IOpenAiManager, OpenAiManager>();
        services.AddScoped<IYnabManager, YnabManager>();
        services.AddSingleton<IAzureKeyVaultService, AzureKeyVaultService>();
        return services;
    }
    
}