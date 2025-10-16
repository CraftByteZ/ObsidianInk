namespace ObsidianInk.Models
{

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } 
        public string Cover { get; set; } = null!;
        public string UrlFile { get; set; } = null!;

        public List<BookAuthor> BookAuthors { get; set; } = null!;
        public List<BookGenre> BookGenres { get; set; } = null!;
        public List<Review> Reviews { get; set; } = null!;
        public List<Order> Orders { get; set; }  = null!;
    }
}