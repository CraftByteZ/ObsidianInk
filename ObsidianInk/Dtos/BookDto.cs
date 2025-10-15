namespace ObsidianInk.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Cover { get; set; }
        public string UrlFile { get; set; }

        public List<int> AuthorIds { get; set; }
        public List<int> GenreIds { get; set; }
    }

}
