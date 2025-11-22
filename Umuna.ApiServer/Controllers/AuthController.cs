using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.Data;
using Umuna.ApiServer.DTOs;
using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            User? user = _context.Users.SingleOrDefault(
                u => u.Name == loginDto.UserName && u.Password == loginDto.Password);

            // Simulate authentication process
            Task.Delay(2000).Wait();

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(
                new { Message = "Login successful", UserName = user.Name, UserId = user.Id });
        }
    }
}