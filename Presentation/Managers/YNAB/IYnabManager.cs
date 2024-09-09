using Domain.Entities;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB
{
    public interface IYnabManager
    {
        Task<YnabTransaction> GenerateYnabTransactionAsync(YnabTransaction ynabTransaction);
    }
}
