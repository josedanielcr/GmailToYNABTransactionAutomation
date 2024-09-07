using Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Moq;

namespace Presentation.Test.Managers
{
    public class OpenAiManagerTests
    {

        private readonly OpenAiManager _sut;
        private readonly Mock<IOpenAiService> _sutMock;
        private readonly Mock<IConfiguration> _sutMockConfig;
        private readonly string _presentationProjectName = "Presentation";
        private readonly string _promptFileName = "prompt.txt";
        private readonly string _solutionDirectory = @"..\..\..\..\";

        public OpenAiManagerTests()
        {
            _sutMock = new Mock<IOpenAiService>();
            _sutMockConfig = new Mock<IConfiguration>();
            _sut = new OpenAiManager(_sutMock.Object, _sutMockConfig.Object);
        }

        private void SetupOpenAiServiceMock(string emailBody, string result)
        {
            _sutMock.Setup(service => service.GenerateTextAsync(emailBody))
                    .ReturnsAsync(result);
        }

        private void SetupIConfigurationMock(string result)
        {
            _sutMockConfig.Setup(config => config["PromptFilePath"]).Returns(result);
        }

        [Fact]
        public async Task GetParsedTransactionFromOpenAiChatGpt_ShouldReturnParsedTransaction_WhenEmailBodyIsValid()
        {
            // Arrange
            var projectDirectory = Directory.GetCurrentDirectory();
            var solutionDirectory = Path.Combine(projectDirectory, _solutionDirectory);
            var promptFilePath = Path.Combine(solutionDirectory, _presentationProjectName, _promptFileName);
            SetupIConfigurationMock(promptFilePath);
            var prompt = await File.ReadAllTextAsync(promptFilePath);

            string emailBody = "Sample email body with transaction details";
            string expectedJson = "{\"amount\": 100.0, \"date\": \"11/10/2024\", \"memo\": \"Purchase\"}";
            SetupOpenAiServiceMock(prompt + emailBody, expectedJson);

            // Act
            var result = await _sut.GetParsedTransactionFromOpenAiChatGpt(emailBody);

            // Assert
            Assert.Equal(expectedJson, result);
        }

        //make a test of argument null exception when the emailBody is null or string.empty
        [Fact]
        public async Task GetParsedTransactionFromOpenAiChatGpt_ShouldThrowArgumentNullException_WhenEmailBodyIsNullOrEmpty()
        {
            // Arrange
            string emailBody = "";
            var projectDirectory = Directory.GetCurrentDirectory();
            var solutionDirectory = Path.Combine(projectDirectory, _solutionDirectory);
            var promptFilePath = Path.Combine(solutionDirectory, _presentationProjectName, _promptFileName);
            SetupIConfigurationMock(promptFilePath);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetParsedTransactionFromOpenAiChatGpt(emailBody));
        }
    }
}