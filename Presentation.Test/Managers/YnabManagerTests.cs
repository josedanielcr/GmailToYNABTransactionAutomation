using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Presentation.Test.Managers
{

    public class YnabManagerTests
    {

        private readonly Mock<IYnabService> _ynabService;
        private readonly Mock<IConfiguration> _configuration;
        private readonly YnabManager _sut;

        public YnabManagerTests()
        {
            _ynabService = new Mock<IYnabService>();
            _configuration = new Mock<IConfiguration>();
            _sut = new YnabManager(_ynabService.Object, _configuration.Object);
        }

        private void SetupYnabServiceMock(string accountId, HttpResponseMessage result)
        {
            _ynabService.Setup(x => x.GenerateTransactionAsync(
                It.IsAny<YnabTransaction>(), accountId)
            ).ReturnsAsync(result);
        }

        private void SetupConfigurationMock()
        { 
            _configuration.Setup(x => x["ynab:accountId"]).Returns("accountId");
        }

        [Fact]
        public async Task GenerateYnabTransactionAsync_ShouldReturnYnabTransaction_WhenValidYnabTransactionIsProvided()
        {
            // Arrange
            var transaction = new YnabTransaction("accountId", DateTime.Now, 100, "memo");
            var jsonContent = new
            {
                Id = "newId",
                AccountId = "accountId",
                Amount = 100,
                Date = DateTime.Now,
                Memo = "memo"
            };
            var contentString = JsonSerializer.Serialize(jsonContent);
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(contentString, Encoding.UTF8, "application/json")
            };
            SetupConfigurationMock();
            SetupYnabServiceMock("accountId", mockResponse);

            // Act
            var result = await _sut.GenerateYnabTransactionAsync(transaction);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("accountId", result.AccountId);
            Assert.Equal(100, result.Amount);
            Assert.Equal("memo", result.Memo);
        }

        [Fact]
        public async Task GenerateYnabTransactionAsync_ShouldThrowArgumentNullException_WhenYnabTransactionIsNull()
        {
            // Arrange
            YnabTransaction transaction = null;

            // Act
            async Task act() => await _sut.GenerateYnabTransactionAsync(transaction);

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

    }
}