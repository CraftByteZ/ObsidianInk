using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObsidianInk.Data;
using ObsidianInk.Dtos;
using ObsidianInk.Models;

namespace ObsidianInk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ObsidianInkContext _context;
        public OrderController(ObsidianInkContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto dto)
        {
            var order = new Order
            {
                DateTime = dto.DateTime,
                Total = dto.Total,
                Status = dto.Status,
                UserId = dto.UserId,
                BookId = dto.BookId
            };

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem($"Unable to create order because the database operation failed: {ex.Message}");
            }

            return Ok();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<OrderDto>>> GetUserOrders(int userId)
        {
            try
            {
                var orders = await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Select(o => new OrderDto
                    {
                        Id = o.Id,
                        UserId = o.UserId,
                        BookId = o.BookId,
                        Total = o.Total,
                        DateTime = o.DateTime,
                        Status = o.Status
                    })
                    .ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return Problem($"Unable to retrieve orders for user {userId} from the database: {ex.Message}");
            }
        }
    }
}