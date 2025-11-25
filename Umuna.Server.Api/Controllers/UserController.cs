using Microsoft.AspNetCore.Mvc;
using Umuna.Core.Contracts.Models;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Server.Infrastructure.Services;

namespace Umuna.ApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromBody] UserCreateDto userDataDto)
        {
            var result = await _userService.AddUserAsync(userDataDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(
                nameof(GetUser),
                new { id = result.Data!.Name }, // route param name matches [HttpGet("{id}")]
                result.Data
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            ServiceResult<UserCreateDto> result = await _userService.GetUserByIdAsync(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

    }
}
