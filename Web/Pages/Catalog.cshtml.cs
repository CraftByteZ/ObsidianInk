using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

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
            var response = await _httpClient.GetFromJsonAsync<List<BookDto>>("api/book/all");
            if (response != null)
                Books = response;
        }
    }
}
