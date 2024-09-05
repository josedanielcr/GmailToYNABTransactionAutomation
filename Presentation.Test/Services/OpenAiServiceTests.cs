using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Moq;
using OpenAI.Chat;

namespace Presentation.Test.Services;

public class OpenAiServiceTests
{
    private readonly OpenAiService _sut;
    private readonly Mock<ChatClient> _chatClientMock;

    public OpenAiServiceTests()
    {
        _chatClientMock = new();
        _sut = new OpenAiService(_chatClientMock.Object);
    }

    private void SetupChatClientMock(string prompt, string result)
    {
    }
    
    [Fact]
    public async Task GenerateTextAsync_ShouldReturnText_WhenValidInput()
    {
        //Arrange
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task GenerateTextAsync_ShouldReturnEmptyString_WhenInvalidInput()
    {
        //Arrange
        
        //Act
        
        //Assert
    }

    [Fact]
    public async Task GenerateTextAsync_ShouldReturnEmptyString_WhenServiceError()
    {
        //Arrange
        
        //Act
        
        //Assert
    }
}