
using Gmail_To_YNAB_Transaction_Automation_API.Services;

namespace Gmail_To_YNAB_Transaction_Automation_API.Managers.OpenAI
{
    public class OpenAiManager : IOpenAiManager
    {
        private readonly IOpenAiService _openAiService;
        private readonly IConfiguration _configuration;
        private readonly string _prompPath = Path.Combine(Directory.GetCurrentDirectory(), "Managers", "OpenAI", "prompt.txt");

        public OpenAiManager(IOpenAiService openAiService, IConfiguration configuration)
        {
            this._openAiService = openAiService;
            this._configuration = configuration;
        }
        public Task<string> GetParsedTransactionFromOpenAiChatGpt(string emailBody)
        {
            if(string.IsNullOrEmpty(emailBody)) throw new ArgumentNullException(emailBody);
            
            string prompt = File.ReadAllText(_prompPath);
            if(string.IsNullOrEmpty(prompt))  throw new ArgumentNullException(prompt);

            // build the prompt
            string finalPrompt = prompt + emailBody;

            return _openAiService.GenerateTextAsync(finalPrompt);
        }
    }
}
