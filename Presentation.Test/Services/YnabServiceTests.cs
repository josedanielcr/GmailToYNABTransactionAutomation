using System.Net;
using System.Text.Json;
using System.Transactions;
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

    public YnabServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri(_baseUri)
        };
        _sut = new YnabService(httpClient);
    }
    
    private void SetupMockResponse(YnabTransaction expectedTransactionResult)
    {
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(expectedTransactionResult), System.Text.Encoding.UTF8, _mediaTypeResponse)
        };
        
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(responseMessage);
    }

    [Fact]
    public async Task GenerateTransactionAsync_ShouldReturnUpdatedTransaction_WhenTransactionInputIsValid()
    {
        // Arrange
        var transaction = new YnabTransaction("id", DateTime.Now, 100, "memo");
        var expectedTransaction = transaction;
        expectedTransaction.Id = "newId";
        SetupMockResponse(expectedTransaction);

        // Act
        var response = await _sut.GenerateTransactionAsync(transaction);

        // Assert
        Assert.Equal(expectedTransaction, response);
    }
    
    [Fact]
    public async Task GenerateTransactionAsync_ShouldThrowArgumentNullException_WhenTransactionIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GenerateTransactionAsync(null));
    }
}