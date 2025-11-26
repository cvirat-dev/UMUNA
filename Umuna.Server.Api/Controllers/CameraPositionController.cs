using Microsoft.AspNetCore.Mvc;
using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Infrastructure.Services;
using Umuna.Core.Contracts.Constants.Api;

namespace Umuna.Server.Api.Controllers
{
    [ApiController]
    [Route(ApiRoutes.CameraPositions.Base)]
    public class CameraPositionController(ICameraPositionService service) : ControllerBase
    {
        private readonly ICameraPositionService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ServiceResult<List<CameraPositionReadDto>> result = await _service.GetAll();

            if (!result.Success)
                return BadRequest(new { result.Message });

            List<CameraPositionReadDto> dtos = result.Data ?? [];
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ServiceResult<CameraPositionReadDto> result = await _service.GetById(id);

            if (!result.Success)
                return NotFound(new { result.Message });

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CameraPositionCreateDto dto)
        {
            ServiceResult<CameraPositionReadDto> result = await _service.Add(dto);

            if (!result.Success)
                return BadRequest(new { result.Message });

            return CreatedAtAction(
                ApiRoutes.CameraPositions.WithId(result.Data!.Id),
                new { id = result.Data!.Id },
                result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CameraPositionUpdateDto dto)
        {
            ServiceResult<CameraPositionReadDto> res = await _service.Update(id, dto);
            
            if (!res.Success)
                return NotFound(new { res.Message });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult res = await _service.Delete(id);
            
            if (!res.Success)
                return NotFound(new { res.Message });

            return NoContent();
        }
    }
}
