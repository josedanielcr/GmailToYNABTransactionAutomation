using Domain.Entities;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Test.Managers
{
    public class TransactionsManagerTests
    {
        private readonly TransactionManager _sut;
        private readonly Mock<IYnabManager> _ynabManagerMock;
        private readonly Mock<IOpenAiManager> _openAiManagerMock;

        public TransactionsManagerTests()
        {
            _ynabManagerMock = new Mock<IYnabManager>();
            _openAiManagerMock = new Mock<IOpenAiManager>();
            _sut = new TransactionManager(_ynabManagerMock.Object, _openAiManagerMock.Object);
        }

        private void SetupOpenAiManagerMock(string jsonTransactionDetails)
        {
            _openAiManagerMock.Setup(x => x.GetParsedTransactionFromOpenAiChatGpt(It.IsAny<string>())).ReturnsAsync(jsonTransactionDetails);
        }

        private void SetupYnabManagerMock(YnabTransaction result)
        {
            _ynabManagerMock.Setup(x => x.GenerateYnabTransactionAsync(It.IsAny<YnabTransaction>())).ReturnsAsync(result);
        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowArgumentNullException_WhenEmailIsNull()
        {
            //Arrange
            Email email = null;

            //Act
            async Task act() => await _sut.GenerateYnabTransactionFromEmail(email);

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(act);
        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowInvalidOperationException_WhenOpenAiManagerResultInvalid()
        {
            //Arrange
            Email email = new Email();
            SetupOpenAiManagerMock(string.Empty);

            //Act
            async Task act() => await _sut.GenerateYnabTransactionFromEmail(email);

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(act);
        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowJsonReaderException_WhenYnabManagerResultInvalid()
        {
            //Arrange
            Email email = new Email();
            SetupOpenAiManagerMock("jsonTransactionDetails");
            SetupYnabManagerMock(null);

            //Act
            async Task act() => await _sut.GenerateYnabTransactionFromEmail(email);

            //Assert
            await Assert.ThrowsAsync<JsonReaderException>(act);
        }
    }
}