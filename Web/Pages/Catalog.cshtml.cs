using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Web.Pages
{
    public class CatalogModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CatalogModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<BookDto> Books { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            Books = await client.GetFromJsonAsync<List<BookDto>>("api/Book/all") ?? new();
        }

        public async Task<IActionResult> OnPostBuyAsync(int bookId)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToPage("/Login");

            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            var client = _httpClientFactory.CreateClient("api");

            var order = new OrderDto
            {
                BookId = bookId,
                UserId = userId,
                DateTime = DateTime.UtcNow,
                Status = "Paid",
                Total = Books.FirstOrDefault(b => b.Id == bookId)?.Price ?? 0
            };

            var response = await client.PostAsJsonAsync("api/Order", order);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Library");

            TempData["Error"] = "Error al procesar la compra.";
            return RedirectToPage();
        }
    }
}
