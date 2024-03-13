using Microsoft.EntityFrameworkCore;
using BookHub.Infrastructure.Data;
using BookHub.Core.Interfaces;
using BookHub.Infrastructure.Repository;
using BookHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Register repositories to the container
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Configure EF Core DbContext
builder.Services.AddDbContext<BookHubDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("BookHubDatabase"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("BookHubDatabase"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();