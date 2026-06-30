using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public ReviewController(ObsidianInkContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(ReviewDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
            {
                return BadRequest($"User with ID {dto.UserId} does not exist.");
            }

            var bookExists = await _context.Books.AnyAsync(b => b.Id == dto.BookId);
            if (!bookExists)
            {
                return BadRequest($"Book with ID {dto.BookId} does not exist.");
            }

            var hasPaidOrder = await _context.Orders.AnyAsync(o =>
                o.UserId == dto.UserId &&
                o.BookId == dto.BookId &&
                o.Status == "Paid");

            if (!hasPaidOrder)
            {
                return BadRequest("The user must have a paid order for this book before leaving a review.");
            }

            var alreadyReviewed = await _context.Reviews.AnyAsync(r =>
                r.UserId == dto.UserId &&
                r.BookId == dto.BookId);

            if (alreadyReviewed)
            {
                return BadRequest("The user has already reviewed this book.");
            }

            var review = new Review
            {
                Comment = dto.Comment,
                Rating = dto.Rating,
                UserId = dto.UserId,
                BookId = dto.BookId
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ReviewDto dto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();
            review.Comment = dto.Comment;
            review.Rating = dto.Rating;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("book/{bookId}")]
        public async Task<ActionResult<List<ReviewDto>>> GetByBook(int bookId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    UserId = r.UserId,
                    BookId = r.BookId,
                    ReviewerUsername = r.User.Username
                }).ToListAsync();
            return Ok(reviews);
        }
    }

}
