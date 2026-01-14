using Microsoft.AspNetCore.Mvc;
using LoginAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace LoginAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // In-memory users (demo only)
        private static List<UserLogin> users = new()
        {
            new UserLogin
            {
                Id = 1,
                Username = "admin",
                Password = "admin123",
                CivilNumber = "12345678",
                IdCardExpiry = "2025-12-31",
                Mobile = "96512345678",
                CardNumber = "1234567890123456"
            }
        };

        private static Dictionary<string, string> otpStore = new();

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

            if (user == null)
                return Unauthorized(new LoginResponse { Success = false, Message = "Invalid credentials" });

            return Ok(new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                Token = GenerateToken(user.Id),
                User = new UserData
                {
                    Id = user.Id,
                    Username = user.Username,
                    CivilNumber = user.CivilNumber
                }
            });
        }

        // Other endpoints stay exactly as you wrote them

        private string GenerateToken(int userId)
        {
            return $"TOKEN_{userId}_{Guid.NewGuid()}";
        }
    }
}
