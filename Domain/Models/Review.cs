using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObsidianInk.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(20) ,MaxLength(1000)]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }
    }

}
