using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.Dtos;
using Umuna.ApiServer.Services;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginDto)
        {
            var user = UserStore.FindUser(loginDto.UserName, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            return Ok(new { Message = "Login successful", UserName = user.UserName, UserId = user.UserId });
        }
    }
}