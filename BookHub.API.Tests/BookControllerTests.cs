using BookHub.API.Controllers;
using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookHub.API.Tests;

public class BookControllerTests
{
    [Fact]
    public async Task GetBooks_ReturnsOkResult_WithBooks()
    {
        // Arrange
        var mockService = new Mock<IBookService>();

        mockService.Setup(service => service.GetAllBooksAsync())
                .ReturnsAsync(new List<Book>());

        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.GetBooks();

        // Assert
        var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Book>>>(result);

        if (actionResult.Result is OkObjectResult okObjectResult)
        {
            Assert.IsType<List<Book>>(okObjectResult.Value);
        }
        else
        {
            Assert.Fail("Expected OkObjectResult");
        }
    }

    [Fact]
    public async Task GetBookByIdAsync_BookExists_ReturnsOkResultWithBook()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBookId = 1;
        var testBook = new Book { Id = testBookId, Title = "Test Book", Author = "Test Author", Price = 10, Stock = 15};

        mockService.Setup(service => service.GetBookByIdAsync(testBookId))
                .ReturnsAsync(testBook);

        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.GetBookById(testBookId);

        // Assert
        var actionResult = Assert.IsAssignableFrom<ActionResult<Book>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnedBook = Assert.IsType<Book>(okResult.Value);

        Assert.Equal(testBookId, returnedBook?.Id);
        Assert.Equal(testBook.Title, returnedBook?.Title);
    }

    [Fact]
    public async Task GetBookByIdAsync_BookDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBookId = 1;

        mockService.Setup(service => service.GetBookByIdAsync(testBookId))
                .ReturnsAsync((Book?)null);
        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.GetBookById(testBookId);

        // Assert
        var actionResult = Assert.IsAssignableFrom<ActionResult<Book>>(result);

        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task CreateBook_ValidBook_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBook = new Book { Title = "New Book", Author = "Test Author", Price = 10, Stock = 15 };
        var expectedBookId = 1;

        mockService.Setup(service => service.CreateBookAsync(It.IsAny<Book>()))
                .ReturnsAsync((Book b) => {
                   b.Id = expectedBookId; // Simulate setting ID upon creation
                   return b;
               });

        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.CreateBook(testBook);

        // Assert
        var actionResult = Assert.IsType<ActionResult<Book>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);

        Assert.Equal("GetBookById", createdAtActionResult.ActionName);
        Assert.IsType<Book>(createdAtActionResult.Value);

        var returnedBook = createdAtActionResult.Value as Book;

        Assert.Equal(expectedBookId, returnedBook?.Id);
        Assert.Equal(testBook.Title, returnedBook?.Title);
    }

    [Fact]
    public async Task CreateBook_InvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var controller = new BooksController(mockService.Object);

        controller.ModelState.AddModelError("Title", "Required");
        
        var testBook = new Book {Title = "New Book", Author = "Test Author", Price = 10, Stock = 15};

        // Act
        var result = await controller.CreateBook(testBook);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        
        Assert.IsType<BadRequestObjectResult>(actionResult);
    }

    [Fact]
    public async Task UpdateBook_ValidRequest_ReturnsNoContent()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBook = new Book { Id = 1, Title = "Updated Title", Author = "Test Author", Price = 10, Stock = 15 };
        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.UpdateBook(testBook.Id, testBook);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateBook_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBook = new Book { Id = 2, Title = "Updated Title", Author = "Test Author", Price = 10, Stock = 15};
        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.UpdateBook(1, testBook); // Simulate ID mismatch

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteBook_BookExists_ReturnsNoContent()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBookId = 1;
        
        mockService.Setup(service => service.GetBookByIdAsync(testBookId))
                .ReturnsAsync(new Book { Id = testBookId, Title = "Updated Title", Author = "Test Author", Price = 10, Stock = 15 });

        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.DeleteBook(testBookId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteBook_BookDoesNotExist_ReturnsNotFound()
    {
        // Arrange
        var mockService = new Mock<IBookService>();
        var testBookId = 1;
        
        mockService.Setup(service => service.GetBookByIdAsync(testBookId))
                   .ReturnsAsync((Book?)null); // Simulate no book found

        var controller = new BooksController(mockService.Object);

        // Act
        var result = await controller.DeleteBook(testBookId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }


}