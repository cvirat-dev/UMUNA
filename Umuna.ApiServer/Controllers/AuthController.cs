using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.Data;
using Umuna.ApiServer.DTOs;

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
            var user = _context.Users.SingleOrDefault(
                u => u.Name == loginDto.UserName && u.Password == loginDto.Password);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(
                new { Message = "Login successful", UserName = user.Name, UserId = user.Id });
        }
    }
}