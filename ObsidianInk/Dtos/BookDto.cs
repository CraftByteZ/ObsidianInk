namespace ObsidianInk.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Cover { get; set; } = string.Empty;
        public string UrlFile { get; set; } = string.Empty;

        // New additions
        public List<string> AuthorNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();

        public List<int> AuthorIds { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
    }

}
