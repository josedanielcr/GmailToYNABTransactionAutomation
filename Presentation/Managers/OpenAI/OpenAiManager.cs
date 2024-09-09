
using Gmail_To_YNAB_Transaction_Automation_API.Services;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI
{
    public class OpenAiManager : IOpenAiManager
    {
        private readonly IOpenAiService _openAiService;
        private readonly IConfiguration _configuration;

        public OpenAiManager(IOpenAiService openAiService, IConfiguration configuration)
        {
            this._openAiService = openAiService;
            this._configuration = configuration;
        }
        public Task<string> GetParsedTransactionFromOpenAiChatGpt(string emailBody)
        {
            if(emailBody == null || emailBody == "") throw new ArgumentNullException(emailBody);

            string prompt = File.ReadAllText(_configuration["PromptFilePath"] ?? "");
            if(prompt == "" || prompt == null)  throw new ArgumentNullException(prompt);

            // build the prompt
            string finalPrompt = prompt + emailBody;

            return _openAiService.GenerateTextAsync(finalPrompt);
        }
    }
}
