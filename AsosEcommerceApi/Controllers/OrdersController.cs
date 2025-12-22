
using AsosEcommerceApi.Db;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsosEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) => _context = context;

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] dynamic data)
        {
            Guid userId = data.userId;
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any()) return BadRequest("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                TotalAmount = cartItems.Sum(c => c.Quantity * 100), // placeholder price
                PaymentStatus = "Pending",
                OrderItems = cartItems.Select(c => new OrderItem
                {
                    ProductVariationId = c.ProductVariationId,
                    Quantity = c.Quantity,
                    Price = 100 // placeholder
                }).ToList()
            };

            await _context.Orders.AddAsync(order);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrders(Guid userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
            return Ok(orders);
        }
    }
}
