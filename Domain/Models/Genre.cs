using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
