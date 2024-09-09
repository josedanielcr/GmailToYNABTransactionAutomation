using System.Net;
using System.Transactions;
using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public interface IYnabService
{
    Task<HttpResponseMessage> GenerateTransactionAsync(YnabTransaction transaction, string budgetId, string accountId);
}