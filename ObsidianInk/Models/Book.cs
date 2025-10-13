namespace ObsidianInk.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public int ReviewId { get; set; }
        public string Cover { get; set; }
        public string UrlFile { get; set; }
    }
}
