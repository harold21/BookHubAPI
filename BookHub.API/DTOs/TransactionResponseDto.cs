namespace BookHub.API.DTOs;

public class TransactionResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsPurchase { get; set; }
}