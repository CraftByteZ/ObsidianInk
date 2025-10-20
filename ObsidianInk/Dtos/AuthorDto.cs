using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Dtos
{
    public class AuthorDto
    {
        public int Id { get; set; }

        [Required, StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;
    }
}
