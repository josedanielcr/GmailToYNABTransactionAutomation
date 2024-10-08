using Azure.Identity;
using Gmail_To_YNAB_Transaction_Automation_API.Services;
using OpenAI.Chat;

namespace Gmail_To_YNAB_Transaction_Automation_API.Configuration;

public static class ExternalServices
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        //azure key vault
        configuration.AddAzureKeyVault( 
            new Uri($"https://{configuration["KeyVaultName"]}.vault.azure.net/"),
            new DefaultAzureCredential());

        //ChatClient of OpenAi chatgpt
        services.AddScoped(sp =>
        {
            var apikey = configuration["openai-api-key"] ?? "";
            var client = new ChatClient(model: "gpt-4o-mini", apikey);
            return client;
        });
        services.AddScoped<IOpenAiService, OpenAiService>();
        
        //Ynab http client and service
        services.AddHttpClient<IYnabService, YnabService>(client =>
        {
            client.BaseAddress = new Uri("https://api.youneedabudget.com/v1/");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["ynab-api-key"]}");
        });
        
        return services;
    }
}