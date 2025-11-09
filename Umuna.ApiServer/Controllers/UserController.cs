using Microsoft.AspNetCore.Mvc;
using Umuna.ApiServer.DTOs;
using Umuna.ApiServer.Services;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDataDto)
        {
            var result = await _userService.AddUserAsync(userDataDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(
                nameof(GetUser), 
                new { userId = result.Data!.Id }, 
                result.Data
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

    }
}
