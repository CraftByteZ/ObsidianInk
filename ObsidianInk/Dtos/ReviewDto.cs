using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }

        [Required, StringLength(500, MinimumLength = 20)]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int Rating { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        public string? ReviewerUsername { get; set; }
    }
}
