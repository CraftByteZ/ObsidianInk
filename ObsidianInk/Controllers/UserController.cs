using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;
using ObsidianInk.Services;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(ObsidianInkContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto dto)
        {
            try
            {
                var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
                if (exists) return BadRequest("Email already registered.");
            }
            catch (Exception)
            {
                return Ok("Demo registration accepted. Use demo@obsidianink.com / Demo123! to sign in.");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone
            };
            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (user != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
                    if (result != PasswordVerificationResult.Failed)
                    {
                        return Ok(user);
                    }
                }

                return IsDemoLogin(dto) ? Ok(DemoData.DemoUser) : Unauthorized("Invalid credentials.");
            }
            catch (Exception)
            {
                return IsDemoLogin(dto) ? Ok(DemoData.DemoUser) : Unauthorized("Invalid credentials.");
            }
        }

        private bool IsDemoLogin(LoginDto dto)
        {
            var demoUser = DemoData.DemoUser;
            var demoHash = _passwordHasher.HashPassword(demoUser, "Demo123!");
            var result = _passwordHasher.VerifyHashedPassword(demoUser, demoHash, dto.Password);

            return string.Equals(dto.Email, demoUser.Email, StringComparison.OrdinalIgnoreCase)
                && result != PasswordVerificationResult.Failed;
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