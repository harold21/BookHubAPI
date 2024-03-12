using Microsoft.EntityFrameworkCore;
using BookHub.Core.Entities;

namespace BookHub.Infrastructure.Data;

public class BookHubDbContext : DbContext
{
    public BookHubDbContext(DbContextOptions<BookHubDbContext> options)
        : base(options)
    {
    }

public DbSet<Book> Books { get; set; }
public DbSet<User> Users { get; set; }
public DbSet<Transaction> Transactions { get; set; }
}