using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Data;
using ObsidianInk.Models;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public BookController(ObsidianInkContext context) => _context = context;

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Cover = book.Cover,
                UrlFile = book.UrlFile,
                AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                GenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList()
            };
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookGenres)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    Cover = b.Cover,
                    UrlFile = b.UrlFile,
                    AuthorIds = b.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                    GenreIds = b.BookGenres.Select(bg => bg.GenreId).ToList()
                }).ToListAsync();

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                Cover = dto.Cover,
                UrlFile = dto.UrlFile,
                BookAuthors = dto.AuthorIds.Select(id => new BookAuthor { AuthorId = id }).ToList(),
                BookGenres = dto.GenreIds.Select(id => new BookGenre { GenreId = id }).ToList()
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.Price = dto.Price;
            book.Cover = dto.Cover;
            book.UrlFile = dto.UrlFile;

            book.BookAuthors = dto.AuthorIds.Select(aid => new BookAuthor { BookId = id, AuthorId = aid }).ToList();
            book.BookGenres = dto.GenreIds.Select(gid => new BookGenre { BookId = id, GenreId = gid }).ToList();

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
