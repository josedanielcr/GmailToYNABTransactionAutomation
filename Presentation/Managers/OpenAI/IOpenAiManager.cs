namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI
{
    public interface IOpenAiManager
    {
        Task<string> GetParsedTransactionFromOpenAiChatGpt(string emailBody);
    }
}
