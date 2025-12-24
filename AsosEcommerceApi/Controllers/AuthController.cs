using AsosEcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AsosEcommerceApi.Data;
using AsosEcommerceApi.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AsosEcommerceApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public AuthController(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _db.Users.AnyAsync(x => x.Email == dto.Email))
                return BadRequest("Email already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RegisteredAt = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("User registered successfully");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return Unauthorized("Invalid credentials");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config["Jwt:Issuer"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = new
                {
                    user.Id,
                    user.Name,
                    user.Email
                }
            });
        }

        // GET LOGGED IN USER
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst("userId")?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _db.Users.FindAsync(Guid.Parse(userId));

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.RegisteredAt
            });
        }
    }
}
