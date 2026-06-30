using ObsidianInk.Dtos;
using ObsidianInk.Models;

namespace ObsidianInk.Services;

public static class DemoData
{
    public static readonly User DemoUser = new()
    {
        Id = 1,
        Username = "demo",
        Email = "demo@obsidianink.local",
        Password = "password123",
        Phone = "555-0100",
        Role = "user"
    };

    public static List<BookDto> Books => new()
    {
        new BookDto
        {
            Id = 1,
            Title = "The Obsidian Library",
            Description = "A curator discovers that every book in a hidden library rewrites itself at midnight.",
            Price = 12.99m,
            Cover = "https://placehold.co/300x450/111827/e5e7eb?text=Obsidian+Library",
            UrlFile = "https://example.com/books/obsidian-library.pdf",
            AuthorIds = new List<int> { 1 },
            GenreIds = new List<int> { 1, 2 },
            AuthorNames = new List<string> { "Mara Vale" },
            GenreNames = new List<string> { "Fantasy", "Mystery" }
        },
        new BookDto
        {
            Id = 2,
            Title = "Ink and Circuitry",
            Description = "A practical guide to building resilient software systems through story-driven examples.",
            Price = 18.50m,
            Cover = "https://placehold.co/300x450/1f2937/f9fafb?text=Ink+%26+Circuitry",
            UrlFile = "https://example.com/books/ink-and-circuitry.pdf",
            AuthorIds = new List<int> { 2 },
            GenreIds = new List<int> { 3 },
            AuthorNames = new List<string> { "Theo Marin" },
            GenreNames = new List<string> { "Technology" }
        },
        new BookDto
        {
            Id = 3,
            Title = "Midnight Margins",
            Description = "Short literary essays about notes, memory, and the books people carry with them.",
            Price = 9.75m,
            Cover = "https://placehold.co/300x450/312e81/e0e7ff?text=Midnight+Margins",
            UrlFile = "https://example.com/books/midnight-margins.pdf",
            AuthorIds = new List<int> { 3 },
            GenreIds = new List<int> { 4 },
            AuthorNames = new List<string> { "Iris Chen" },
            GenreNames = new List<string> { "Essays" }
        }
    };

    public static List<OrderDto> OrdersForUser(int userId) => userId == DemoUser.Id
        ? new List<OrderDto>
        {
            new OrderDto
            {
                Id = 1,
                UserId = DemoUser.Id,
                BookId = 1,
                Total = 12.99m,
                DateTime = DateTime.UtcNow.AddDays(-1),
                Status = "Paid"
            }
        }
        : new List<OrderDto>();
}
