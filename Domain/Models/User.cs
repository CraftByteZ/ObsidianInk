namespace ObsidianInk.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } = "user";

        public List<Order> Orders { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
    }

}
