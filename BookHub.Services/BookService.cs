using BookHub.Core.Entities;
using BookHub.Core.Interfaces;

namespace BookHub.Services;

public class BookService : IBookService
{
    public readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        await _bookRepository.AddAsync(book);
        
        return book;
    }

    public async Task UpdateBookAsync(Book book)
    {
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteAsync(id);
    }
}
