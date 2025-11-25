using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class CameraPositionService(ICameraPositionRepository cameraPositionRepository) : ICameraPositionService
    {
        private readonly ICameraPositionRepository _cameraPositionRepository = cameraPositionRepository;

        public async Task<ServiceResult<CameraPositionReadDto>> Add(CameraPositionCreateDto createDto)
        {
            if (createDto == null)
                return ServiceResult<CameraPositionReadDto>.Fail("Create DTO is null.");

            if (string.IsNullOrWhiteSpace(createDto.PositionName))
                return ServiceResult<CameraPositionReadDto>.Fail("Position name is required.");

            if (createDto.Position == null)
                return ServiceResult<CameraPositionReadDto>.Fail("Position is required.");

            if (createDto.Rotation == null)
                return ServiceResult<CameraPositionReadDto>.Fail("Rotation is required.");

            try
            {
                CameraPosition cameraPosition = createDto.ToEntity();
                await _cameraPositionRepository.Add(cameraPosition);
                return ServiceResult<CameraPositionReadDto>.Ok(cameraPosition.ToDto());
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> Delete(string id)
        {
            if (!int.TryParse(id, out var cameraPositionId))
                return ServiceResult.Fail("Invalid camera position ID format.");

            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetById(cameraPositionId);
                if (cameraPosition == null)
                    return ServiceResult.Fail("Camera position not found.");

                await _cameraPositionRepository.Delete(cameraPositionId);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<CameraPositionReadDto>>> GetAll()
        {
            try
            {
                List<CameraPosition> cameraPositions = await _cameraPositionRepository.GetAll();
                return ServiceResult<List<CameraPositionReadDto>>.Ok(cameraPositions.ToDtoList());
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPositionReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> GetById(string id)
        {
            if (!int.TryParse(id, out var cameraPositionId))
                return ServiceResult<CameraPositionReadDto>.Fail("Invalid camera position ID format.");

            try
            {
                var cameraPosition = await _cameraPositionRepository.GetById(cameraPositionId);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("Camera position not found.");

                return ServiceResult<CameraPositionReadDto>.Ok(cameraPosition.ToDto());
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<CameraPositionReadDto>>> GetByUserId(string userId)
        {
            if (!int.TryParse(userId, out var userIdInt))
                return ServiceResult<List<CameraPositionReadDto>>.Fail("Invalid user ID format.");

            try
            {
                List<CameraPosition> cameraPositions = await _cameraPositionRepository.GetByUserId(userIdInt);
                return ServiceResult<List<CameraPositionReadDto>>.Ok(cameraPositions.ToDtoList());
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPositionReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> GetDefaultForUser(string userId)
        {
            if (!int.TryParse(userId, out var userIdInt))
                return ServiceResult<CameraPositionReadDto>.Fail("Invalid user ID format.");

            try
            {
                var cameraPosition = await _cameraPositionRepository.GetDefaultForUser(userIdInt);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("No default camera position found for user.");

                return ServiceResult<CameraPositionReadDto>.Ok(cameraPosition.ToDto());
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> Update(string id, CameraPositionUpdateDto updateDto)
        {
            if (!int.TryParse(id, out var cameraPositionId))
                return ServiceResult<CameraPositionReadDto>.Fail("Invalid camera position ID format.");

            if (updateDto == null)
                return ServiceResult<CameraPositionReadDto>.Fail("Update DTO is null.");

            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetById(cameraPositionId);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("Camera position not found.");

                await _cameraPositionRepository.Update(cameraPosition.Update(updateDto));
                return ServiceResult<CameraPositionReadDto>.Ok(cameraPosition.ToDto());
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }
    }
}
