using Domain.Entities;

namespace Application.Transactions;

public interface ITransactionManager
{
    Task ProcessTransactionAsync(Email email);
}