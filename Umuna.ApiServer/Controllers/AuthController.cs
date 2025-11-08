using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.Data;
using Umuna.ApiServer.DTOs;
using Umuna.ApiServer.Services;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            var user = _context.Users.SingleOrDefault(
                u => u.PlayerName == loginDto.UserName && u.PlayerPassword == loginDto.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(
                new { Message = "Login successful", UserName = user.PlayerName, UserId = user.Id });
        }
    }
}