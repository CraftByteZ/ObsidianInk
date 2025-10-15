namespace ObsidianInk.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pending";

        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }

}
