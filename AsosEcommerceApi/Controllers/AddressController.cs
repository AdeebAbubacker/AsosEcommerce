using AsosEcommerceApi.Data;
using AsosEcommerceApi.DTO;
using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/addresses")]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AddressController(AppDbContext db)
        {
            _db = db;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst("userId")!.Value);
        }

        // ADD ADDRESS
        [HttpPost]
        public async Task<IActionResult> Add(AddAddressDto dto)
        {
            var address = new Address
            {
                UserId = GetUserId(),
                AddressLine = dto.AddressLine,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            };

            _db.Address.Add(address);
            await _db.SaveChangesAsync();

            return Ok("Address added successfully");
        }

        // GET USER ADDRESSES
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = GetUserId();
            var addresses = await _db.Address
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return Ok(addresses);
        }

        // UPDATE ADDRESS
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AddAddressDto dto)
        {
            var address = await _db.Address.FindAsync(id);

            if (address == null || address.UserId != GetUserId())
                return NotFound();

            address.AddressLine = dto.AddressLine;
            address.City = dto.City;
            address.State = dto.State;
            address.PostalCode = dto.PostalCode;
            address.Country = dto.Country;

            await _db.SaveChangesAsync();
            return Ok("Address updated");
        }

        // DELETE ADDRESS
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var address = await _db.Address.FindAsync(id);

            if (address == null || address.UserId != GetUserId())
                return NotFound();

            _db.Address.Remove(address);
            await _db.SaveChangesAsync();

            return Ok("Address deleted");
        }
    }
}