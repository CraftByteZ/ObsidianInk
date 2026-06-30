using ObsidianInk.Models;

namespace ObsidianInk.Data;

public static class DbInitializer
{
    public static void Seed(ObsidianInkContext context)
    {
        context.Database.EnsureCreated();

        if (context.Books.Any())
        {
            return;
        }

        var authors = new[]
        {
            new Author { Name = "Mara Blackwood" },
            new Author { Name = "Elias Stone" },
            new Author { Name = "Nora Vale" },
            new Author { Name = "Julian Cross" },
            new Author { Name = "Iris Night" }
        };

        var genres = new[]
        {
            new Genre { Name = "Fantasy" },
            new Genre { Name = "Mystery" },
            new Genre { Name = "Science Fiction" },
            new Genre { Name = "Romance" },
            new Genre { Name = "Horror" }
        };

        var books = new[]
        {
            new Book
            {
                Title = "Inkbound Kingdom",
                Description = "A royal archivist discovers a spell hidden in the margins of an ancient book.",
                Price = 14.99m,
                Cover = "https://placehold.co/400x600?text=Inkbound+Kingdom",
                UrlFile = "demo/inkbound-kingdom.pdf"
            },
            new Book
            {
                Title = "The Last Lantern",
                Description = "A detective follows a trail of impossible clues through a city that never sleeps.",
                Price = 12.99m,
                Cover = "https://placehold.co/400x600?text=The+Last+Lantern",
                UrlFile = "demo/the-last-lantern.pdf"
            },
            new Book
            {
                Title = "Stars Below Zero",
                Description = "Explorers cross a frozen moon to find a signal older than humanity.",
                Price = 16.99m,
                Cover = "https://placehold.co/400x600?text=Stars+Below+Zero",
                UrlFile = "demo/stars-below-zero.pdf"
            },
            new Book
            {
                Title = "Letters in the Rain",
                Description = "Two strangers rebuild their lives through letters left in a quiet bookshop.",
                Price = 9.99m,
                Cover = "https://placehold.co/400x600?text=Letters+in+the+Rain",
                UrlFile = "demo/letters-in-the-rain.pdf"
            },
            new Book
            {
                Title = "House of Hollow Pages",
                Description = "A family returns to a haunted estate where every room tells a different story.",
                Price = 13.99m,
                Cover = "https://placehold.co/400x600?text=House+of+Hollow+Pages",
                UrlFile = "demo/house-of-hollow-pages.pdf"
            }
        };

        books[0].BookAuthors.Add(new BookAuthor { Book = books[0], Author = authors[0] });
        books[1].BookAuthors.Add(new BookAuthor { Book = books[1], Author = authors[1] });
        books[2].BookAuthors.Add(new BookAuthor { Book = books[2], Author = authors[2] });
        books[3].BookAuthors.Add(new BookAuthor { Book = books[3], Author = authors[3] });
        books[4].BookAuthors.Add(new BookAuthor { Book = books[4], Author = authors[4] });

        books[0].BookGenres.Add(new BookGenre { Book = books[0], Genre = genres[0] });
        books[1].BookGenres.Add(new BookGenre { Book = books[1], Genre = genres[1] });
        books[2].BookGenres.Add(new BookGenre { Book = books[2], Genre = genres[2] });
        books[3].BookGenres.Add(new BookGenre { Book = books[3], Genre = genres[3] });
        books[4].BookGenres.Add(new BookGenre { Book = books[4], Genre = genres[4] });

        var demoUser = new User
        {
            Username = "demo",
            Email = "demo@obsidianink.com",
            Password = "Demo123!",
            Phone = "8090000000",
            Role = "user"
        };

        var order = new Order
        {
            User = demoUser,
            Book = books[0],
            Total = books[0].Price,
            Status = "Paid"
        };

        var reviews = new[]
        {
            new Review
            {
                User = demoUser,
                Book = books[0],
                Rating = 5,
                Comment = "A polished demo title with great pacing and a memorable fantasy hook."
            },
            new Review
            {
                User = demoUser,
                Book = books[1],
                Rating = 4,
                Comment = "An engaging mystery sample that works well for a local presentation."
            }
        };

        context.Authors.AddRange(authors);
        context.Genres.AddRange(genres);
        context.Books.AddRange(books);
        context.Users.Add(demoUser);
        context.Orders.Add(order);
        context.Reviews.AddRange(reviews);

        context.SaveChanges();
    }
}
