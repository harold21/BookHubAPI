using Microsoft.AspNetCore.Mvc;
using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookHub.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET: /books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        return Ok(books);
    }

    // GET: /books/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    // POST: /books
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdBook = await _bookService.CreateBookAsync(book);

        return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
    }

    // PUT: /books/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _bookService.UpdateBookAsync(book);

        return NoContent();
    }

    // DELETE: /books/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        
        if (book == null)
        {
            return NotFound();
        }

        await _bookService.DeleteBookAsync(id);

        return NoContent();
    }
}