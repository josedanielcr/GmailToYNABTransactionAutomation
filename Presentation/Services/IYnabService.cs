using System.Net;
using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public interface IYnabService
{
    Task<HttpStatusCode> GenerateTransactionAsync(YnabTransaction transaction);
}