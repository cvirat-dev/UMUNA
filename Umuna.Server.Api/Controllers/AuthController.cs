using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.DTOs.Requests;
using Umuna.Server.Infrastructure.Database;
using Umuna.Server.Domain.Entities;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(UmunaDbContext context) : ControllerBase
    {
        private readonly UmunaDbContext _context = context;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            User? user = _context.Users.SingleOrDefault(
                u => u.Name == loginDto.UserName && u.PasswordHash == loginDto.Password);

            // Simulate authentication process
            Task.Delay(2000).Wait();

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(
                new { Message = "Login successful", UserName = user.Name, UserId = user.Id });
        }
    }
}