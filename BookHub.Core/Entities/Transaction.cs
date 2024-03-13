namespace BookHub.Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required int BookId { get; set; }
    public Book? Book { get; set; }
    public required int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public required bool IsPurchase { get; set; } // True for buy, false for sell
}
