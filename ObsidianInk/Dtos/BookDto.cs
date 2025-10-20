using System.ComponentModel.DataAnnotations;

namespace ObsidianInk.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required, StringLength(200, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(1000, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [Range(0.01, 9999.99, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Url, Required(ErrorMessage = "La portada es requerida.")]
        public string Cover { get; set; } = string.Empty;

        [Url, Required(ErrorMessage = "El archivo del libro es requerido.")]
        public string UrlFile { get; set; } = string.Empty;

        public List<string> AuthorNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();

        [MinLength(1, ErrorMessage = "Debe seleccionar al menos un autor.")]
        public List<int> AuthorIds { get; set; } = new();

        [MinLength(1, ErrorMessage = "Debe seleccionar al menos un género.")]
        public List<int> GenreIds { get; set; } = new();
    }
}
