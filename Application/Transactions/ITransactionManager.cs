using Domain.Entities;

namespace Application.Transactions;

public interface ITransactionManager
{
    Task ReceiveEmailTransaction(Email email);
}