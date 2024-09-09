using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using System.Text.Json;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB
{
    public class YnabManager : IYnabManager
    {
        private readonly IYnabService _ynabService;
        private readonly IConfiguration _configuration;

        public YnabManager(IYnabService ynabService, IConfiguration configuration)
        {
            this._ynabService = ynabService;
            this._configuration = configuration;
        }
        public async Task<YnabTransaction> GenerateYnabTransactionAsync(YnabTransaction ynabTransaction)
        {
            ArgumentNullException.ThrowIfNull(ynabTransaction);
            var budgetId = _configuration["ynab-budget"];
            var accountId = _configuration["ynab-account"];

            if(string.IsNullOrEmpty(budgetId))
            {
                throw new InvalidOperationException("Failed to get account id from configuration");
            }
            
            if(string.IsNullOrEmpty(accountId))
            {
                throw new InvalidOperationException("Failed to get account id from configuration");
            }

            var result = await _ynabService.GenerateTransactionAsync(ynabTransaction, budgetId, accountId) 
                ?? throw new InvalidOperationException("Failed to generate transaction");

            var content = await result.Content.ReadAsStringAsync();
            var deserializedTransaction = JsonSerializer.Deserialize<YnabTransaction>(content)
                   ?? throw new JsonException("Failed to deserialize YnabTransaction.");

            return deserializedTransaction;
        }
    }
}
