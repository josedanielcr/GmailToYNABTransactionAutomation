using System.Net;
using System.Text;
using System.Text.Json;
using System.Transactions;
using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Converters;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class YnabService: IYnabService
{
    private readonly string _mediaTypeResponse = "application/json";
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly HttpClient _client;
    private readonly int multiplier = 1000;

    public YnabService(HttpClient client)
    {
        _client = client;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonDateConverter()
            }
        };
    }

    public async Task<HttpResponseMessage> GenerateTransactionAsync(YnabTransaction transaction, string budgetId, string accountId)
    {
        if (string.IsNullOrEmpty(budgetId)) throw new ArgumentNullException(nameof(budgetId));
        if (string.IsNullOrEmpty(accountId)) throw new ArgumentNullException(nameof(accountId));
        ArgumentNullException.ThrowIfNull(transaction);
        transaction.AccountId = accountId;
        string endpoint = $"budgets/{budgetId}/transactions";
        var transactionRequest = BuildTransactionRequest(transaction);
        var result = await _client.PostAsync(endpoint, new StringContent(JsonSerializer.Serialize(transactionRequest), Encoding.UTF8, _mediaTypeResponse));
        return result;
    }

    private object BuildTransactionRequest(YnabTransaction transaction)
    {
        return new
        {
            transaction = new
            {
                account_id = transaction.AccountId,
                date = transaction.Date,
                amount = transaction.Amount * multiplier,
                memo = transaction.Memo,
                cleared = transaction.Cleared
            }
        };
    }
}