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

            var bookResponse = await client.GetAsync($"api/Book/{bookId}");
            var book = bookResponse.IsSuccessStatusCode
                ? await bookResponse.Content.ReadFromJsonAsync<BookDto>()
                : null;

            if (book is null)
            {
                TempData["Error"] = "No se encontró el libro seleccionado.";
                return RedirectToPage();
            }

            var order = new OrderDto
            {
                BookId = bookId,
                UserId = userId,
                DateTime = DateTime.UtcNow,
                Status = "Paid",
                Total = book.Price
            };

            var response = await client.PostAsJsonAsync("api/Order", order);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Library");

            TempData["Error"] = "Error al procesar la compra.";
            return RedirectToPage();
        }
    }
}
