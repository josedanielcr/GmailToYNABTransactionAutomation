using Application.Transactions;

namespace Gmail_To_YNAB_Transaction_Automation_API.Configuration;

public static class InternalServices
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionManager, TransactionManager>();
        return services;
    }
    
}