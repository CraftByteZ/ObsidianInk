using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Models
{
    public class BookAuthor
    {
        [Key]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Key]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

}
