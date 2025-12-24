using AsosEcommerceApi.Data;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _db;
        public BrandController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Brands.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            brand.Id = Guid.NewGuid();
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();
            return Ok(brand);
        }
    }

}
