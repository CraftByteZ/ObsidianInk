using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;
using ObsidianInk.Dtos;
using ObsidianInk.Models;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public BookController(ObsidianInkContext context) => _context = context;

        // ==============================
        // GET: api/book/{id}
        // ==============================
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            var dto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                Cover = book.Cover,
                UrlFile = book.UrlFile,
                AuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                GenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList(),
                AuthorNames = book.BookAuthors.Select(ba => ba.Author.Name).ToList(),
                GenreNames = book.BookGenres.Select(bg => bg.Genre.Name).ToList()
            };

            return Ok(dto);
        }

        // ==============================
        // GET: api/book/all
        // ==============================
        [HttpGet("all")]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            var books = await _context.Books
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    Cover = b.Cover,
                    UrlFile = b.UrlFile,
                    AuthorIds = b.BookAuthors.Select(ba => ba.AuthorId).ToList(),
                    GenreIds = b.BookGenres.Select(bg => bg.GenreId).ToList(),
                    AuthorNames = b.BookAuthors.Select(ba => ba.Author.Name).ToList(),
                    GenreNames = b.BookGenres.Select(bg => bg.Genre.Name).ToList()
                })
                .ToListAsync();

            return Ok(books);
        }

        // ==============================
        // POST: api/book
        // ==============================
        [HttpPost]
        public async Task<IActionResult> Create(BookDto dto)
        {
            if (dto == null)
                return BadRequest();

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
            return Ok(book.Id);
        }

        // ==============================
        // PUT: api/book/{id}
        // ==============================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDto dto)
        {
            var book = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookGenres)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
                return NotFound();

            book.Title = dto.Title;
            book.Description = dto.Description;
            book.Price = dto.Price;
            book.Cover = dto.Cover;
            book.UrlFile = dto.UrlFile;

            // Replace authors and genres
            book.BookAuthors = dto.AuthorIds.Select(aid => new BookAuthor { BookId = id, AuthorId = aid }).ToList();
            book.BookGenres = dto.GenreIds.Select(gid => new BookGenre { BookId = id, GenreId = gid }).ToList();

            await _context.SaveChangesAsync();
            return Ok();
        }

        // ==============================
        // DELETE: api/book/{id}
        // ==============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
