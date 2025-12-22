
using AsosEcommerceApi.Db;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsosEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context) => _context = context;

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItem item)
        {
            var exists = await _context.CartItems.FirstOrDefaultAsync(c =>
                c.UserId == item.UserId && c.ProductVariationId == item.ProductVariationId);
            if (exists != null) exists.Quantity += item.Quantity;
            else await _context.CartItems.AddAsync(item);

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            var cart = await _context.CartItems
                .Where(c => c.UserId == userId).ToListAsync();
            return Ok(cart);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItem(Guid cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) return NotFound();
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
