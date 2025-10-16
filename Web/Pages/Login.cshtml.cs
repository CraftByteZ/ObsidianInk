using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public LoginDto Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("api/Auth/login", Login);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Catalog");

            ModelState.AddModelError("", "Credenciales inválidas");
            return Page();
        }
    }
}
