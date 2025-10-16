using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public UserDto User { get; set; } = new UserDto();

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("api/Auth/register", User);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("/Login");

            ModelState.AddModelError("", "Error al registrarse");
            return Page();
        }
    }
}
