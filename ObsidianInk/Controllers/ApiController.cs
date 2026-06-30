using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ObsidianInk.Data;
using ObsidianInk.Dtos;
using ObsidianInk.Models;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ObsidianInkContext _context;

        public ApiController(ObsidianInkContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password, // ⚠️ Hashear en producción
                Phone = dto.Phone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registered successfully");
        }

            // Iniciar sesión (crear cookie)
            [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

                if (user == null)
                    return Unauthorized(new { message = "Credenciales inválidas" });

                // Solo devuelve los datos del usuario (sin cookies)
                return Ok(new
                {
                    message = "Login exitoso",
                    username = user.Username,
                    userId = user.Id,
                    email = user.Email
                });
            }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logged out");
        }
    }
}
