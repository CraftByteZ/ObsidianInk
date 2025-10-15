using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public AuthorController(ObsidianInkContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAll()
        {
            var authors = await _context.Authors
                .Select(a => new AuthorDto { Id = a.Id, Name = a.Name })
                .ToListAsync();
            return Ok(authors);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AuthorDto dto)
        {
            var author = new Author { Name = dto.Name };
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, AuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            author.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
