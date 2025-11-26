using Microsoft.AspNetCore.Mvc;
using Umuna.Core.Contracts.Constants.Api;
using Umuna.Core.Contracts.DTOs.Requests;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Infrastructure.Services;

namespace Umuna.Server.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Auth.Base)]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            ServiceResult<UserReadDto> result = await _authService.Authenticate(loginDto);

            if (!result.Success)
                return Unauthorized("Invalid username or password.");

            return Ok(result.Data);
        }
    }
}