using Domain.Entities;

namespace Application.Transactions;

public class TransactionManager : ITransactionManager
{
    public Task ReceiveEmailTransaction(Email email)
    {
        throw new NotImplementedException();
    }
}