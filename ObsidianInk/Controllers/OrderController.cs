using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Dtos;
using ObsidianInk.Models;
using ObsidianInk.Data;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public OrderController(ObsidianInkContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            var order = new Order
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                Total = dto.Total,
                Status = "Pending",
                DateTime = DateTime.UtcNow
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            order.Status = status;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderDto>>> GetByUser(int userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    BookId = o.BookId,
                    UserId = o.UserId,
                    Total = o.Total,
                    Status = o.Status,
                    DateTime = o.DateTime
                }).ToListAsync();
            return Ok(orders);
        }
    }

}
