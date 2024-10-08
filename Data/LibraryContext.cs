
    // Data/LibraryContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookLibrarySystem.Models;

namespace BookLibrarySystem.Data
{
    /// <summary>
    /// Represents the library's database context.
    /// </summary>
    public class LibraryContext : DbContext
    {
        private readonly ILogger<LibraryContext> _logger;

        public LibraryContext(DbContextOptions<LibraryContext> options, ILogger<LibraryContext> logger) : base(options)
        {
            _logger = logger;
        }

        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Seed initial data for the in-memory database.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Log seeding information
            _logger.LogInformation("Seeding initial data for the in-memory database.");
            modelBuilder.Entity<Book>().HasData(
                 new Book { Id = 1, Title = "The Great Gatsby", Writer = "F. Scott Fitzgerald", BookIdentifier = "12345", IsAvailable = true },
 new Book { Id = 2, Title = "Moby Dick", Writer = "Herman Melville", BookIdentifier = "67890", IsAvailable = true },
 new Book { Id = 3, Title = "Pride and Prejudice", Writer = "Jane Austen", BookIdentifier = "11121", IsAvailable = true }
          );
        }
    }
}