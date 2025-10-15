namespace ObsidianInk.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public int BookId { get; set; }
    }

}
