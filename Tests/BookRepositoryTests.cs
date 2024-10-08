// Tests/BookRepositoryTests.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using BookLibrarySystem.Data;
using BookLibrarySystem.Models;
using BookLibrarySystem.Repositories;

public class BookRepositoryTests
{
    private readonly LibraryContext _context;
    private readonly BookRepository _repository;
    private readonly Mock<ILogger<BookRepository>> _loggerMock;
    private readonly LibraryContext _logger;
    public BookRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
        .UseInMemoryDatabase("TestLibraryDB")
        .Options;

       //_context = new LibraryContext(options);
        _loggerMock = new Mock<ILogger<BookRepository>>();
        _repository = new BookRepository(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllBooks_ReturnsAllBooks()
    {
        // Arrange
        _context.Books.Add(new Book { Id = 1, Title = "Test Book", Writer = "Test Author", BookIdentifier = "1234567890", IsAvailable = true });
        await _context.SaveChangesAsync();

        // Log the operation
        _loggerMock.Setup(logger => logger.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()));

        // Act
        var books = await _repository.GetAllBooksAsync();

        // Assert
        _loggerMock.Verify(logger => logger.LogInformation("Retrieving all books from the database."), Times.Once);
        Assert.Single(books);
    }

    [Fact]
    public async Task GetBookById_ReturnsBook()
    {
        // Arrange
        var book = new Book { Id = 2, Title = "Another Test Book", Writer = "Another Author", BookIdentifier = "0987654321", IsAvailable = true };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        var retrievedBook = await _repository.GetBookByIdAsync(book.Id);

        // Assert
        _loggerMock.Verify(logger => logger.LogInformation($"Retrieving book with ID: {book.Id}"), Times.Once);
        Assert.NotNull(retrievedBook);
        Assert.Equal(book.Title, retrievedBook.Title);
    }

    [Fact]
    public async Task AddBook_AddsBookSuccessfully()
    {
        // Arrange
        var newBook = new Book { Title = "New Book", Writer = "New Author", BookIdentifier = "1111111111", IsAvailable = true };

        // Act
        var addedBook = await _repository.AddBookAsync(newBook);

        // Assert
        _loggerMock.Verify(logger => logger.LogInformation($"Adding a new book: {newBook.Title}"), Times.Once);
        Assert.NotNull(addedBook);
        Assert.Equal(newBook.Title, addedBook.Title);
    }

    [Fact]
    public async Task UpdateBook_UpdatesBookSuccessfully()
    {
        // Arrange
        var book = new Book { Id = 3, Title = "Update Test Book", Writer = "Update Author", BookIdentifier = "2222222222", IsAvailable = true };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        book.Title = "Updated Title";
        await _repository.UpdateBookAsync(book);

        // Assert
        _loggerMock.Verify(logger => logger.LogInformation($"Updating book with ID: {book.Id}"), Times.Once);
        var updatedBook = await _repository.GetBookByIdAsync(book.Id);
        Assert.Equal("Updated Title", updatedBook.Title);
    }

    [Fact]
    public async Task DeleteBook_DeletesBookSuccessfully()
    {
        // Arrange
        var book = new Book { Id = 4, Title = "Delete Test Book", Writer = "Delete Author", BookIdentifier = "3333333333", IsAvailable = true };
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteBookAsync(book.Id);

        // Assert
        _loggerMock.Verify(logger => logger.LogInformation($"Deleting book with ID: {book.Id}"), Times.Once);
        var deletedBook = await _repository.GetBookByIdAsync(book.Id);
        Assert.Null(deletedBook);
    }
}