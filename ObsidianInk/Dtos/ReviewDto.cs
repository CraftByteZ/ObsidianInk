namespace ObsidianInk.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }
    }

}
