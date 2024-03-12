namespace BookHub.Core.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsPurchase { get; set; } // True for buy, false for sell
}
