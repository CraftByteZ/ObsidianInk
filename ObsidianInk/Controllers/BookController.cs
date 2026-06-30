using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Services;

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
            Book? book;
            try
            {
                book = await _context.Books
                    .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                    .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                    .FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception)
            {
                var demoBook = DemoData.Books.FirstOrDefault(b => b.Id == id);
                return demoBook == null ? NotFound() : Ok(demoBook);
            }

            if (book == null)
            {
                var demoBook = DemoData.Books.FirstOrDefault(b => b.Id == id);
                return demoBook == null ? NotFound() : Ok(demoBook);
            }

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
            try
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

                return Ok(books.Any() ? books : DemoData.Books);
            }
            catch (Exception)
            {
                return Ok(DemoData.Books);
            }
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
        }
    }