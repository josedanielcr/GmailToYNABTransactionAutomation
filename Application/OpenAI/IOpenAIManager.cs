namespace Application.OpenAI;

public interface IOpenAIManager
{
    Task<string> ProcessEmailToTransactionJsonFormat(string email);
}