using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using ObsidianInk.Dtos;

namespace Web.Pages
{
    public class CatalogModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public List<BookDto> Books { get; set; } = new();

        public CatalogModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<BookDto>>("api/book");
            if (response != null)
                Books = response;
        }
    }
}
