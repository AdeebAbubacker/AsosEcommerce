using AsosEcommerceApi.Data;
using AsosEcommerceApi.DTO;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ProductController(AppDbContext db) => _db = db;

        // Browse + Filter
        [HttpGet]
        public async Task<IActionResult> Get(
     Guid? categoryId,
     Guid? brandId,
     string? size)
        {
            var query = _db.Products.AsQueryable();

            if (categoryId != null)
                query = query.Where(p => p.CategoryId == categoryId);

            if (brandId != null)
                query = query.Where(p => p.BrandId == brandId);

            if (!string.IsNullOrEmpty(size))
            {
                // Filter by size through variations
                query = query.Where(p => _db.ProductVariations
                    .Any(v => v.ProductId == p.Id && v.Size == size));
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")] // optional
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            // 1️⃣ Create Product
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                BrandId = dto.BrandId
            };

            _db.Products.Add(product);

            // 2️⃣ Create Variations
            foreach (var v in dto.Variations)
            {
                var variation = new ProductVariation
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Size = v.Size,
                    Color = v.Color,
                    Stock = v.Stock
                };

                _db.ProductVariations.Add(variation);
            }

            await _db.SaveChangesAsync();

            return Ok(new
            {
                message = "Product created successfully",
                productId = product.Id
            });
        }

    }





}
