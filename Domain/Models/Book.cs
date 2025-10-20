using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Models
{

    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public string Cover { get; set; }
        public string UrlFile { get; set; }

        public List<BookAuthor> BookAuthors { get; set; } = new();
        public List<BookGenre> BookGenres { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();
        public List<Order> Orders { get; set; }  = new();
    }
}