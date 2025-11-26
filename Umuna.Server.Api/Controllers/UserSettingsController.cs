using Microsoft.AspNetCore.Mvc;
using Umuna.Core.Contracts.Constants.Api;
using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Infrastructure.Services;

namespace Umuna.Server.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutes.UserSettings.Base)]
    public class UserSettingsController(IUserSettingsService service) : ControllerBase
    {
        private readonly IUserSettingsService _service = service;

        /// <summary>
        /// Get user settings by user ID
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            ServiceResult<SettingsReadDto> result = await _service.GetByUserId(userId);
            
            if (!result.Success)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        /// <summary>
        /// Create new user settings
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SettingsCreateDto dto)
        {
            ServiceResult<SettingsReadDto> result = await _service.Add(dto);
            
            if (!result.Success)
                return BadRequest(new { result.Message });

            return CreatedAtAction(nameof(GetByUserId), new { userId = result.Data!.UserId }, result.Data);
        }

        /// <summary>
        /// Update user settings
        /// </summary>
        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(int userId, [FromBody] SettingsUpdateDto dto)
        {
            ServiceResult<SettingsReadDto> existingResult = await _service.GetByUserId(userId);
            
            if (!existingResult.Success)
                return NotFound(new { existingResult.Message });

            ServiceResult<SettingsReadDto> result = await _service.Update(existingResult.Data!.Id, dto);
            
            if (!result.Success)
                return BadRequest(new { result.Message });

            return NoContent();
        }

        /// <summary>
        /// Delete user settings
        /// </summary>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            ServiceResult<SettingsReadDto> existingResult = await _service.GetByUserId(userId);
            
            if (!existingResult.Success)
                return NotFound(new { existingResult.Message });

            ServiceResult result = await _service.Delete(existingResult.Data!.Id);
            
            if (!result.Success)
                return BadRequest(new { result.Message });

            return NoContent();
        }
    }
}
