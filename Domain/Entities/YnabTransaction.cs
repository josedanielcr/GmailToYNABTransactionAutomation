namespace Domain.Entities;

public class YnabTransaction(string accountId, DateTime date, decimal amount, string memo)
{
    public string AccountId { get; set; } = accountId;
    public DateTime Date { get; set; } = date;
    public decimal Amount { get; set; } = amount;
    public string Memo { get; set; } = memo;
    public string Cleared { get; set; } = "uncleared";
}