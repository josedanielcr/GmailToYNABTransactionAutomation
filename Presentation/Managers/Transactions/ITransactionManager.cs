using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions
{
    public interface ITransactionManager
    {
        Task GenerateYnabTransactionFromEmail(Email email);
    }
}
