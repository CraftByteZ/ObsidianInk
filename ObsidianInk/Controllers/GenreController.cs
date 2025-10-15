using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public GenreController(ObsidianInkContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<GenreDto>>> GetAll()
        {
            var genres = await _context.Genres
                .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
                .ToListAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, GenreDto dto)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            genre.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
