using System.Net;
using System.Text;
using System.Text.Json;
using System.Transactions;
using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class YnabService(HttpClient client) : IYnabService
{
    private readonly string _mediaTypeResponse = "application/json";

    public async Task<HttpResponseMessage> GenerateTransactionAsync(YnabTransaction transaction, string budgetId)
    {
        if (string.IsNullOrEmpty(budgetId)) throw new ArgumentNullException(nameof(budgetId));
        ArgumentNullException.ThrowIfNull(transaction);
        string endpoint = $"/budgets/{budgetId}/transactions";
        var result = await client.PostAsync(endpoint, new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, _mediaTypeResponse));
        return result;
    }
}