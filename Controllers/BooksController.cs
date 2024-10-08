// Controllers/BooksController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookLibrarySystem.Models;
using BookLibrarySystem.Repositories;

namespace BookLibrarySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookRepository bookRepository, ILogger<BooksController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all books from the library.
        /// </summary>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            _logger.LogInformation("Received request to retrieve all books.");
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a specific book by ID.
        /// </summary>
        /// 
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            _logger.LogInformation($"Received request to retrieve book with ID: {id}");
            var book = await _bookRepository.GetBookByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        /// <summary>
        /// Adds a new book to the library.
        /// </summary>
        /// 

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _logger.LogInformation($"Received request to add a new book: {book.Title}");
            var createdBook = await _bookRepository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, createdBook);
        }

        /// <summary>
        /// Updates an already existing book.
        /// </summary>
        /// 

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                _logger.LogWarning("Mismatch between ID in URL and ID in book object.");
                return BadRequest();
            }
            _logger.LogInformation($"Updating book with ID: {id}");
            await _bookRepository.UpdateBookAsync(book);
            return NoContent();
        }

        /// <summary>
        /// Deletes a book from the book library.
        /// </summary>
        ///
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation($"Received request to delete book with ID: {id}");
            await _bookRepository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}