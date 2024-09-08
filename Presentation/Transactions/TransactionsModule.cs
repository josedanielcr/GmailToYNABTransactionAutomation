using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gmail_To_YNAB_Transaction_Automation_API.Transactions;

public static class TransactionsModule
{
    public static void AddTransactionsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/transaction", async (Email email, ITransactionManager manager) =>
        {
            await manager.GenerateYnabTransactionFromEmail(email);
        });
    }
}