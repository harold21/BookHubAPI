using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using BookHub.Infrastructure.Data;

namespace BookHub.Services;

public class TransactionService : ITransactionService
{
    public readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    private readonly BookHubDbContext _dbContext;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, IBookRepository bookRepository, BookHubDbContext dbContext)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
    {
        return await _transactionRepository.GetAllAsync();
    }

    public async Task<Transaction?> GetTransactionByIdAsync(int id)
    {
        return await _transactionRepository.GetByIdAsync(id);
    }

    public async Task<Transaction> CreateTransactionAsync(int userId, int bookId, int quantity, bool isPurchase)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try 
        {

            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            if (isPurchase && book.Stock < quantity)
            {
                throw new InvalidOperationException($"Not enough stock for book ID {bookId}. Requested: {quantity}, Available: {book.Stock}");
            }

            book.Stock += isPurchase ? quantity : -quantity;

            await _bookRepository.UpdateAsync(book);

            var transactionObj = new Transaction
            {
                UserId = userId,
                BookId = bookId,
                Quantity = quantity,
                IsPurchase = isPurchase,
                TransactionDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transactionObj);

            await transaction.CommitAsync();

            return transactionObj;

        }
        catch
        {
            await transaction.RollbackAsync();

            throw;
        }
    }
}
