namespace BookHub.API.DTOs;

public class CreateTransactionDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public bool IsPurchase { get; set; }
}