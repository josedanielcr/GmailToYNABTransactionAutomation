using Domain.Entities;

namespace Application.Transactions;

public class TransactionManager : ITransactionManager
{
    public Task ProcessTransactionAsync(Email email)
    {
        throw new NotImplementedException();
    }
}