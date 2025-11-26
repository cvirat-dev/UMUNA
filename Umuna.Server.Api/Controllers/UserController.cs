using Microsoft.AspNetCore.Mvc;
using Umuna.Core.Contracts.Constants.Api;
using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Infrastructure.Services;

namespace Umuna.Server.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Users.Base)]
    public class UserController(
        IUserService userService,
        ICameraPositionService cameraPositionService,
        IUserSettingsService userSettingsService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly ICameraPositionService _cameraPositionService = cameraPositionService;
        private readonly IUserSettingsService _userSettingsService = userSettingsService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResult<List<UserReadDto>> result = await _userService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            ServiceResult<UserReadDto> result = await _userService.GetById(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}/camera-positions")]
        public async Task<IActionResult> GetCameraPositionsByUser(int id)
        {
            ServiceResult<List<CameraPositionReadDto>> result = await _cameraPositionService.GetByUserId(id);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}/camera-positions/default")]
        public async Task<IActionResult> GetDefaultCameraPositionByUser(int id)
        {
            ServiceResult<CameraPositionReadDto> result = await _cameraPositionService.GetDefaultForUser(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}/settings")]
        public async Task<IActionResult> GetUserSettingsByUser(int id)
        {
            ServiceResult<SettingsReadDto> result = await _userSettingsService.GetByUserId(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userDataDto)
        {
            ServiceResult<UserReadDto> result = await _userService.Add(userDataDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            UserReadDto? data = result.Data;
            if(data == null)
            {
                return BadRequest("User creation failed.");
            }

            return CreatedAtAction(
                ApiRoutes.Users.WithId(data.Id),
                new { id = data.Name },
                data
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto updateDto)
        {
            ServiceResult<UserReadDto> result = await _userService.Update(id, updateDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult result = await _userService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }

    }
}
