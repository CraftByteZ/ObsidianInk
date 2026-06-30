using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            Login = new LoginDto();
        }

        [BindProperty]
        public LoginDto Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("api/Auth/login", Login);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return Page();
            }

            var result = await response.Content.ReadFromJsonAsync<UserDto>();

            // // Crear claims
            // var claims = new List<UserDto>
            // {
            //     new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
            //     new Claim(ClaimTypes.Name, result.Username),
            //     new Claim(ClaimTypes.Email, result.Email)
            // };

            // var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            // var principal = new ClaimsPrincipal(identity);

            // await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Index");
        }
    }
}
