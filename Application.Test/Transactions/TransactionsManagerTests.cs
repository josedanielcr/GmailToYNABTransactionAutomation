using Application.OpenAI;
using Application.Transactions;
using Application.YNAB;
using Moq;

namespace Application.Test.Transactions;

public class TransactionsManagerTests
{
    private readonly TransactionManager _sut;
    private readonly Mock<IYnabManager> _mockYnabManager = new();
    private readonly Mock<IOpenAIManager> _mockOpenAIManager = new();

    public TransactionsManagerTests()
    {
        _sut = new TransactionManager(_mockYnabManager.Object, _mockOpenAIManager.Object);
    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldReturnTransaction_WhenEmailIsValid()
    {

    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldThrowException_WhenEmailIsInvalid()
    {

    }

    [Fact]
    public async Task ProcessTransactionAsync_ShouldThrowException_WhenEmailIsNull()
    {

    }

}