namespace ObsidianInk.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public double TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
