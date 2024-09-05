namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public interface IOpenAiService
{
    Task<string> GenerateTextAsync(string? prompt);
}