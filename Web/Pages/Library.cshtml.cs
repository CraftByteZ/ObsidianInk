using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class LibraryModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public List<BookDto> Books { get; set; } = new();

        public LibraryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task OnGetAsync()
        {
            // Puedes cambiar el endpoint cuando tengas autenticación
            var response = await _httpClient.GetFromJsonAsync<List<BookDto>>("api/book");
            if (response != null)
                Books = response.Take(3).ToList(); // ejemplo: solo mostrar algunos libros
        }
    }
}
