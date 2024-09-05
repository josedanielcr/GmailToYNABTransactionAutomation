using System.Net;
using System.Net.Http.Json;
using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Moq;
using Moq.Protected;

namespace Presentation.Test.Services;

public class YnabServiceTests
{
    private readonly YnabService _sut;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

    public YnabServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.youneedabudget.com/v1/")
        };
        _sut = new YnabService(httpClient);
    }
    
    private void SetupMockResponse(HttpStatusCode statusCode)
    {
        var response = new HttpResponseMessage(statusCode);
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
    }

    [Fact]
    public async Task GenerateTransactionAsync_ShouldReturnOK_WhenValidTransactionInput()
    {
        // Arrange
        var transaction = new YnabTransaction("id", DateTime.Now, 100, "memo");
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
        SetupMockResponse(HttpStatusCode.OK);

        // Act
        var response = await _sut.GenerateTransactionAsync(transaction);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response);
    }
    
    [Fact]
    public async Task GenerateTransactionAsync_ShouldReturnBadRequest_WhenInvalidTransactionInput()
    {
        // Arrange
        var transaction = new YnabTransaction("id", DateTime.Now, 100, "memo");
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
        SetupMockResponse(HttpStatusCode.BadRequest);

        // Act
        var response = await _sut.GenerateTransactionAsync(transaction);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response);
    }
    
    [Fact]
    public async Task GenerateTransactionAsync_ShouldReturnInternalServerError_WhenServiceError()
    {
        // Arrange
        var transaction = new YnabTransaction("id", DateTime.Now, 100, "memo");
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        SetupMockResponse(HttpStatusCode.InternalServerError);

        // Act
        var response = await _sut.GenerateTransactionAsync(transaction);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response);
    }
    
    [Fact]
    public async Task GenerateTransactionAsync_ShouldThrowArgumentNullException_WhenTransactionIsNull()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GenerateTransactionAsync(null));
    }
}