using Microsoft.AspNetCore.Mvc;
using LoginAPI.Data;
using LoginAPI.Models;
using LoginAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null ||
                !PasswordHasher.Verify(request.Password, user.Password))
                return Unauthorized("Invalid credentials");

            return Ok("Login Successful");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRequest request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (existingUser != null)
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = request.Username,
                Password = PasswordHasher.Hash(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully");
        }

    }
}
