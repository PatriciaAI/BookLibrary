// Repositories/BookRepository.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookLibrarySystem.Data;
using BookLibrarySystem.Models;
using BookLibrarySystem.Repositories;

namespace BookLibrarySystem.Repositories
{
    /// <summary>
    /// Implements the IBookRepository interface.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(LibraryContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            _logger.LogInformation("Retrieving all books from the database.");
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            _logger.LogInformation($"Retrieving book with ID: {id}");
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _logger.LogInformation($"Adding a new book: {book.Title}");
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _logger.LogInformation($"Updating book with ID: {book.Id}");
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            _logger.LogInformation($"Deleting book with ID: {id}");
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}