using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ObsidianInk.Dtos;
using System.Net.Http.Json;
using System.Text.Json;

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

        public string AlertMessage { get; set; } = "";
        public string AlertType { get; set; } = "";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                AlertMessage = "Por favor, corrige los errores antes de continuar.";
                AlertType = "danger";
                return Page();
            }

            var client = _httpClientFactory.CreateClient("api");
            var response = await client.PostAsJsonAsync("api/Auth/register", User);

            if (response.IsSuccessStatusCode)
            {
                AlertMessage = "¡Registro exitoso! Ya puedes iniciar sesión.";
                AlertType = "success";
                return RedirectToPage("/Login");
            }

            // Intentar leer respuesta del backend
            var errorContent = await response.Content.ReadAsStringAsync();

            try
            {
                var problemDetails = JsonSerializer.Deserialize<ValidationProblemDetails>(errorContent);

                if (problemDetails?.Errors != null)
                {
                    foreach (var kv in problemDetails.Errors)
                        foreach (var msg in kv.Value)
                            ModelState.AddModelError(kv.Key, msg);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error al registrarse. Verifica los datos.");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error inesperado en el registro.");
            }

            AlertMessage = "Error al registrarse. Revisa los campos.";
            AlertType = "danger";
            return Page();
        }
    }
}
