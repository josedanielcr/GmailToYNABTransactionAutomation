
using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions
{
    public class TransactionManager : ITransactionManager
    {
        private readonly IYnabManager _ynabManager;
        private readonly IOpenAiManager _openAiManager;

        public TransactionManager(IYnabManager ynabManager, IOpenAiManager openAiManager)
        {
            this._ynabManager = ynabManager;
            this._openAiManager = openAiManager;
        }

        public Task GenerateYnabTransactionFromEmail(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
