using System.Net;
using System.Transactions;
using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class YnabService : IYnabService
{
    private readonly HttpClient _client;

    public YnabService(HttpClient client)
    {
        _client = client;
    }
    
    public Task<YnabTransaction> GenerateTransactionAsync(YnabTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        throw new NotImplementedException();
    }
}