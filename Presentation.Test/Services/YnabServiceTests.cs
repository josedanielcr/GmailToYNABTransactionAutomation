using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Moq;
using Moq.Protected;

namespace Presentation.Test.Services;

public class YnabServiceTests
{
    private readonly YnabService _sut;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly string _baseUri = "https://api.youneedabudget.com/v1/";
    private readonly string _mediaTypeResponse = "application/json";
    private readonly string _myBudgetId = "myBudget";

    public YnabServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(_baseUri)
        };
        _sut = new YnabService(httpClient);
    }
    
    private void SetupMockResponse(HttpResponseMessage expectedTransactionResult)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(expectedTransactionResult);
    }

    [Fact]
    public async Task GenerateTransactionAsync_ShouldReturnUpdatedTransaction_WhenTransactionInputIsValid()
    {
        // Arrange
        var transaction = new YnabTransaction("id", DateTime.Now, 100, "memo");
        var expectedTransaction = transaction;
        expectedTransaction.Id = "newId";
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(expectedTransaction), Encoding.UTF8, _mediaTypeResponse)
        };
        SetupMockResponse(mockResponse);

        // Act
        var response = await _sut.GenerateTransactionAsync(transaction,_myBudgetId);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        var actualTransaction = JsonSerializer.Deserialize<YnabTransaction>(responseContent);
        Assert.NotNull(actualTransaction);
        Assert.Equal(expectedTransaction.Id, actualTransaction.Id);
        Assert.Equal(expectedTransaction.Date, actualTransaction.Date);
        Assert.Equal(expectedTransaction.Amount, actualTransaction.Amount);
        Assert.Equal(expectedTransaction.Memo, actualTransaction.Memo);
    }
    
    [Fact]
    public async Task GenerateTransactionAsync_ShouldThrowArgumentNullException_WhenTransactionIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GenerateTransactionAsync(null,_myBudgetId));
    }
}