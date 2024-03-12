

using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using BookHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookHub.Infrastructure.Repository;

public class BookRepository : IBookRepository
{
    private readonly BookHubDbContext _context;

    public BookRepository(BookHubDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.FindAsync(id);
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book != null)
        {
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }
    }
}
