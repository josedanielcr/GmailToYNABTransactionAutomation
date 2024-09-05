using Gmail_To_YNAB_Transaction_Automation_API.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Presentation.Test.Services;

public class KeyVaultServiceTests
{
    private readonly AzureKeyVaultService _sut; // System Under Test
    private readonly Mock<IConfiguration> _configurationMock;

    public KeyVaultServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _sut = new AzureKeyVaultService(_configurationMock.Object);
    }
    
    private void SetupConfiguration(string secretName, string secretValue)
    {
        _configurationMock.Setup(config => config[secretName]).Returns(secretValue);
    }
    
    [Fact]
    public void GetSecret_ShouldReturnSecret_WhenSecretExists()
    {
        // Arrange
        var secretName = "testing-secret";
        var expectedSecretValue = "testingSecretVal";
        SetupConfiguration(secretName, expectedSecretValue);
        // Act
        var secret = _sut.GetSecret(secretName);
        // Assert
        Assert.Equal(expectedSecretValue, secret);
    }

    [Fact]
    public void GetSecret_ShouldReturnEmptyString_WhenSecretDoesNotExist()
    {
        // Arrange
        var secretName = "testing-secret";
        var expectedSecretValue = "";
        SetupConfiguration(secretName, expectedSecretValue);
        // Act
        var secret = _sut.GetSecret(secretName);
        // Assert
        Assert.Equal(secret, expectedSecretValue);
    }
}