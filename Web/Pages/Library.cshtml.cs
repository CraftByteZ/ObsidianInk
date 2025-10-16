using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Web.Pages
{
    [Authorize]
    public class LibraryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LibraryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<BookDto> MyBooks { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            // 1️⃣ Get user orders
            var orders = await client.GetFromJsonAsync<List<OrderDto>>($"api/Order/user/{userId}");
            if (orders == null || !orders.Any()) return;

            // 2️⃣ Get all books
            var books = await client.GetFromJsonAsync<List<BookDto>>("api/Book/all");

            // 3️⃣ Filter by purchased
            MyBooks = books?.Where(b => orders.Any(o => o.BookId == b.Id)).ToList() ?? new();
        }
    }
}
