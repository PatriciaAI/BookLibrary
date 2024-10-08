// Repositories/IBookRepository.cs
using BookLibrarySystem.Models;

namespace BookLibrarySystem.Repositories
{
    /// <summary>
    /// Interface for book repository operations.
    /// </summary>
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}