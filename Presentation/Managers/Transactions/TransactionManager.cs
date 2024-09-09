
using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;
using Newtonsoft.Json;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IYnabManager _ynabManager;
        private readonly IOpenAiManager _openAiManager;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public TransactionManager(IYnabManager ynabManager, IOpenAiManager openAiManager)
        {
            this._ynabManager = ynabManager;
            this._openAiManager = openAiManager;
            this._jsonSerializerSettings = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy"
            };
        }
        
        public async Task GenerateYnabTransactionFromEmail(Email email)
        {
            ArgumentNullException.ThrowIfNull(email);
            var jsonTransactionDetails = await _openAiManager.GetParsedTransactionFromOpenAiChatGpt(email.Body)
                ?? throw new InvalidOperationException("OpenAi response is invalid");

            var deserializedTransaction = JsonConvert.DeserializeObject<YnabTransaction>(jsonTransactionDetails,_jsonSerializerSettings)
                ?? throw new InvalidOperationException("deserialization of transaction is invalid");

            _ = await _ynabManager.GenerateYnabTransactionAsync(deserializedTransaction)
                ?? throw new InvalidOperationException("ynab transaction api request response is invalid");
        }
    }
}
