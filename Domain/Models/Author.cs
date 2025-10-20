using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
