using BookHub.Core.Entities;

namespace BookHub.Core.Interfaces;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    Task<Transaction?> GetTransactionByIdAsync(int id);
    Task<Transaction> CreateTransactionAsync(int userId, int bookId, int quantity, bool isPurchase);
}