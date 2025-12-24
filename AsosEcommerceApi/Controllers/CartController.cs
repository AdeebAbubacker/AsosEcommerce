using AsosEcommerceApi.Data;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Guid variationId, int qty)
        {
            var userId = Guid.Parse(User.FindFirst("userId")!.Value);

            // 1️⃣ Check if cart exists
            var cart = await _db.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                _db.Carts.Add(cart);
                await _db.SaveChangesAsync();
            }

            // 2️⃣ Check if item already in cart
            var existingItem = await _db.CartItems.FirstOrDefaultAsync(ci =>
                ci.CartId == cart.Id &&
                ci.ProductVariationId == variationId);

            if (existingItem != null)
            {
                existingItem.Quantity += qty;
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductVariationId = variationId,
                    Quantity = qty
                };

                _db.CartItems.Add(cartItem);
            }

            await _db.SaveChangesAsync();
            return Ok("Item added to cart");
        }
    }
}