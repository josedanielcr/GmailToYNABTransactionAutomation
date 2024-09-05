using OpenAI.Chat;

namespace Gmail_To_YNAB_Transaction_Automation_API.Services;

public class OpenAiService(ChatClient chatClient) : IOpenAiService
{
    public async Task<string> GenerateTextAsync(string? prompt)
    {
        if(prompt == null) return "";
        UserChatMessage message = new UserChatMessage(prompt);
        ChatCompletion completion = await chatClient.CompleteChatAsync(message);
        return completion.Content.ToString() ?? "";
    }
}