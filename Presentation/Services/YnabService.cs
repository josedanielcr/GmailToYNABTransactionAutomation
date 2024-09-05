using System.Net;
using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class YnabService : IYnabService
{
    private readonly HttpClient _client;

    public YnabService(HttpClient client)
    {
        _client = client;
    }
    
    public Task<HttpStatusCode> GenerateTransactionAsync(YnabTransaction transaction)
    {
        throw new NotImplementedException();
    }
}