using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Range(0.01, 999999.99)]
        public decimal Total { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = "Pending";

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
