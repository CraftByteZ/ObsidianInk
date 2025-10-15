using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        [BindProperty]
        public LoginDto Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _httpClient.PostAsJsonAsync("api/user/login", Input);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Catalog");

            ModelState.AddModelError(string.Empty, "Credenciales inválidas");
            return Page();
        }
    }
}
