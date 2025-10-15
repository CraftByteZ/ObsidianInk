using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("api");
        }

        [BindProperty]
        public UserDto Input { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _httpClient.PostAsJsonAsync("api/user", Input);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Login");

            ModelState.AddModelError(string.Empty, "Error al registrar usuario");
            return Page();
        }
    }
}
