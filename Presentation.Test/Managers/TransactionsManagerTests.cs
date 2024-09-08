using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.Transactions;
using Gmail_To_YNAB_Transaction_Automation_API.Managers.YNAB;
using Moq;
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
        private readonly Mock<OpenAiManager> _openAiManagerMock;

        public TransactionsManagerTests()
        {
            _ynabManagerMock = new Mock<IYnabManager>();
            _openAiManagerMock = new Mock<OpenAiManager>();
            _sut = new TransactionManager(_ynabManagerMock.Object, _openAiManagerMock.Object);
        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowArgumentNullException_WhenEmailIsNull()
        {
            //Arrange
        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowInvalidOperationException_WhenOpenAiManagerInvalid()
        {

        }

        [Fact]
        public async Task GenerateYnabTransactionFromEmail_ShouldThrowInvalidOperationException_WhenYnabManagerInvalid()
        {

        }
    }
}
