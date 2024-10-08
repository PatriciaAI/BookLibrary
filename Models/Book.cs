
// Models/Book.cs
namespace BookLibrarySystem.Models
{
    /// <summary>
    /// Represents a book in the library.
    /// </summary>
    public class Book
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Writer { get; set; }

        public string? BookIdentifier { get; set; }

        public bool IsAvailable { get; set; }
    }
}