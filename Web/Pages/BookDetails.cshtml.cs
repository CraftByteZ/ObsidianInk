using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class BookDetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public BookDetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public BookDto? Book { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Book = await _httpClient.GetFromJsonAsync<BookDto>($"api/book/{Id}");
            if (Book == null)
                return NotFound();
            return Page();
        }
    }
}
