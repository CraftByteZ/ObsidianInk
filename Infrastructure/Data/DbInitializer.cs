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
            new Author { Name = "Iris Night" },
            new Author { Name = "Theo Ravensong" },
            new Author { Name = "Selene Ashcroft" },
            new Author { Name = "Cassian Vale" }
        };

        var genres = new[]
        {
            new Genre { Name = "Fantasy" },
            new Genre { Name = "Mystery" },
            new Genre { Name = "Science Fiction" },
            new Genre { Name = "Romance" },
            new Genre { Name = "Horror" },
            new Genre { Name = "Adventure" },
            new Genre { Name = "Historical" },
            new Genre { Name = "Thriller" }
        };

        var books = new[]
        {
            new Book
            {
                Title = "Inkbound Kingdom",
                Description = "A royal archivist discovers a spell hidden in the margins of an ancient book.",
                Price = 14.99m,
                Cover = "https://placehold.co/400x600/1f2937/f8fafc?text=Inkbound%0AKingdom",
                UrlFile = "demo/inkbound-kingdom.pdf"
            },
            new Book
            {
                Title = "The Last Lantern",
                Description = "A detective follows a trail of impossible clues through a city that never sleeps.",
                Price = 12.99m,
                Cover = "https://placehold.co/400x600/f59e0b/111827?text=The+Last%0ALantern",
                UrlFile = "demo/the-last-lantern.pdf"
            },
            new Book
            {
                Title = "Stars Below Zero",
                Description = "Explorers cross a frozen moon to find a signal older than humanity.",
                Price = 16.99m,
                Cover = "https://placehold.co/400x600/0ea5e9/e0f2fe?text=Stars+Below%0AZero",
                UrlFile = "demo/stars-below-zero.pdf"
            },
            new Book
            {
                Title = "Letters in the Rain",
                Description = "Two strangers rebuild their lives through letters left in a quiet bookshop.",
                Price = 9.99m,
                Cover = "https://placehold.co/400x600/7c3aed/f5f3ff?text=Letters+in%0Athe+Rain",
                UrlFile = "demo/letters-in-the-rain.pdf"
            },
            new Book
            {
                Title = "House of Hollow Pages",
                Description = "A family returns to a haunted estate where every room tells a different story.",
                Price = 13.99m,
                Cover = "https://placehold.co/400x600/111827/f97316?text=House+of%0AHollow+Pages",
                UrlFile = "demo/house-of-hollow-pages.pdf"
            },
            new Book
            {
                Title = "The Clockwork Orchard",
                Description = "An apprentice botanist tends mechanical trees that bloom with memories of the future.",
                Price = 15.49m,
                Cover = "https://placehold.co/400x600/047857/d1fae5?text=Clockwork%0AOrchard",
                UrlFile = "demo/the-clockwork-orchard.pdf"
            },
            new Book
            {
                Title = "Velvet Eclipse",
                Description = "A masked pianist and an ambitious astronomer race to stop a citywide omen.",
                Price = 11.99m,
                Cover = "https://placehold.co/400x600/be123c/ffe4e6?text=Velvet%0AEclipse",
                UrlFile = "demo/velvet-eclipse.pdf"
            },
            new Book
            {
                Title = "Map of the Drowned Sky",
                Description = "Sky pirates chart vanished constellations to uncover a kingdom beneath the sea.",
                Price = 17.99m,
                Cover = "https://placehold.co/400x600/1d4ed8/dbeafe?text=Drowned%0ASky",
                UrlFile = "demo/map-of-the-drowned-sky.pdf"
            },
            new Book
            {
                Title = "A Lullaby for Iron Wolves",
                Description = "A frontier medic protects a caravan from enchanted machines that hunt by moonlight.",
                Price = 18.49m,
                Cover = "https://placehold.co/400x600/57534e/fef3c7?text=Iron%0AWolves",
                UrlFile = "demo/a-lullaby-for-iron-wolves.pdf"
            },
            new Book
            {
                Title = "The Sapphire Conspiracy",
                Description = "A museum curator uncovers a royal forgery that could topple three empires.",
                Price = 13.49m,
                Cover = "https://placehold.co/400x600/2563eb/ecfeff?text=Sapphire%0AConspiracy",
                UrlFile = "demo/the-sapphire-conspiracy.pdf"
            }
        };

        AddAuthors(books[0], authors[0]);
        AddAuthors(books[1], authors[1]);
        AddAuthors(books[2], authors[2]);
        AddAuthors(books[3], authors[3]);
        AddAuthors(books[4], authors[4]);
        AddAuthors(books[5], authors[5], authors[6]);
        AddAuthors(books[6], authors[6]);
        AddAuthors(books[7], authors[7], authors[0]);
        AddAuthors(books[8], authors[5]);
        AddAuthors(books[9], authors[1], authors[7]);

        AddGenres(books[0], genres[0], genres[5]);
        AddGenres(books[1], genres[1], genres[7]);
        AddGenres(books[2], genres[2], genres[5]);
        AddGenres(books[3], genres[3]);
        AddGenres(books[4], genres[4], genres[1]);
        AddGenres(books[5], genres[0], genres[2]);
        AddGenres(books[6], genres[3], genres[7]);
        AddGenres(books[7], genres[5], genres[0]);
        AddGenres(books[8], genres[2], genres[4], genres[5]);
        AddGenres(books[9], genres[6], genres[1], genres[7]);

        var demoUser = new User
        {
            Username = "demo",
            Email = "demo@obsidianink.com",
            Password = "Demo123!",
            Phone = "8090000000",
            Role = "user"
        };

        var adminUser = new User
        {
            Username = "admin",
            Email = "admin@obsidianink.com",
            Password = "Admin123!",
            Phone = "8090000001",
            Role = "admin"
        };

        var orders = new[]
        {
            new Order { User = demoUser, Book = books[0], Total = books[0].Price, Status = "Paid" },
            new Order { User = demoUser, Book = books[5], Total = books[5].Price, Status = "Paid" },
            new Order { User = demoUser, Book = books[9], Total = books[9].Price, Status = "Paid" }
        };

        var reviews = new[]
        {
            new Review { User = demoUser, Book = books[0], Rating = 5, Comment = "The world feels rich from the first chapter, and the archivist's journey is easy to follow." },
            new Review { User = demoUser, Book = books[1], Rating = 4, Comment = "A quiet mystery with a strong atmosphere and a detective story that moves at a steady pace." },
            new Review { User = demoUser, Book = books[5], Rating = 5, Comment = "The mechanical orchard is a memorable setting, and the story has a warm sense of wonder." },
            new Review { User = adminUser, Book = books[7], Rating = 5, Comment = "A fast adventure with clear stakes, vivid locations, and a strong sense of discovery." },
            new Review { User = adminUser, Book = books[8], Rating = 4, Comment = "Moody and tense, with a good balance between science fiction elements and survival drama." },
            new Review { User = demoUser, Book = books[9], Rating = 5, Comment = "The historical intrigue works well, and the plot keeps building without becoming confusing." }
        };

        context.Authors.AddRange(authors);
        context.Genres.AddRange(genres);
        context.Books.AddRange(books);
        context.Users.AddRange(demoUser, adminUser);
        context.Orders.AddRange(orders);
        context.Reviews.AddRange(reviews);

        context.SaveChanges();
    }

    private static void AddAuthors(Book book, params Author[] authors)
    {
        foreach (var author in authors)
        {
            book.BookAuthors.Add(new BookAuthor { Book = book, Author = author });
        }
    }

    private static void AddGenres(Book book, params Genre[] genres)
    {
        foreach (var genre in genres)
        {
            book.BookGenres.Add(new BookGenre { Book = book, Genre = genre });
        }
    }
}