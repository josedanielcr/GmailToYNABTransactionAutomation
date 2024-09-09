using Application.OpenAI;
using Application.YNAB;
using Domain.Entities;

namespace Application.Transactions;

public class TransactionManager : ITransactionManager
{
    private readonly IYnabManager _ynabManager;
    private readonly IOpenAIManager _openAIManager;

    public TransactionManager(IYnabManager ynabManager, IOpenAIManager openAIManager)
    {
        this._ynabManager = ynabManager;
        this._openAIManager = openAIManager;
    }
    public Task ProcessTransactionAsync(Email email)
    {
        throw new NotImplementedException();
    }
}