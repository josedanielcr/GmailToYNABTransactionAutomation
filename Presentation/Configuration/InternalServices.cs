using Application.Transactions;
using Gmail_To_YNAB_Transaction_Automation_API.Services;

namespace Gmail_To_YNAB_Transaction_Automation_API.Configuration;

public static class InternalServices
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddSingleton<IAzureKeyVaultService, AzureKeyVaultService>();
        return services;
    }
    
}