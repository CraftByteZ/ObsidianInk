using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public UserController(ObsidianInkContext context) => _context = context;

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            if (exists) return BadRequest("Email already registered.");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password, // ⚠️ En producción, usa hashing
                Phone = dto.Phone
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            return user == null ? Unauthorized("Invalid credentials.") : Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone
            };
        }
    }

}
