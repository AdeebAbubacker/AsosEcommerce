using AsosEcommerceApi.Data;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _db.Categories.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            category.Id = Guid.NewGuid();
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return Ok(category);
        }
    }

}
