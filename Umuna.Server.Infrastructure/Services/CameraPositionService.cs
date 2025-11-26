using Umuna.Core.Contracts.DTOs.CameraPositions;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class CameraPositionService(
        ICameraPositionRepository cameraPositionRepository,
        IEntityToDtoMapper<CameraPosition, CameraPositionReadDto, CameraPositionCreateDto, CameraPositionUpdateDto> entityToDtoMapper
        ) : ICameraPositionService
    {
        private readonly ICameraPositionRepository _cameraPositionRepository = cameraPositionRepository;
        private readonly IEntityToDtoMapper<
            CameraPosition,
            CameraPositionReadDto,
            CameraPositionCreateDto,
            CameraPositionUpdateDto>
            _entityToDtoMapper = entityToDtoMapper;

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
                CameraPosition cameraPosition = _entityToDtoMapper.ToEntity(createDto);
                await _cameraPositionRepository.Add(cameraPosition);
                CameraPositionReadDto dto = _entityToDtoMapper.ToDto(cameraPosition);
                return ServiceResult<CameraPositionReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetById(id);
                if (cameraPosition == null)
                    return ServiceResult.Fail("Camera position not found.");

                await _cameraPositionRepository.Delete(id);
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
                List<CameraPositionReadDto> dtos = _entityToDtoMapper.ToDtoList(cameraPositions);
                return ServiceResult<List<CameraPositionReadDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPositionReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> GetById(int id)
        {
            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetById(id);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("Camera position not found.");

                CameraPositionReadDto dto = _entityToDtoMapper.ToDto(cameraPosition);
                return ServiceResult<CameraPositionReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<CameraPositionReadDto>>> GetByUserId(int userId)
        {
            try
            {
                List<CameraPosition> cameraPositions = await _cameraPositionRepository.GetByUserId(userId);
                List<CameraPositionReadDto> dtos = _entityToDtoMapper.ToDtoList(cameraPositions);
                return ServiceResult<List<CameraPositionReadDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<CameraPositionReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> GetDefaultForUser(int userId)
        {
            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetDefaultForUser(userId);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("No default camera position found for user.");

                CameraPositionReadDto dto = _entityToDtoMapper.ToDto(cameraPosition);
                return ServiceResult<CameraPositionReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<CameraPositionReadDto>> Update(int id, CameraPositionUpdateDto updateDto)
        {
            if (updateDto == null)
                return ServiceResult<CameraPositionReadDto>.Fail("Update DTO is null.");

            try
            {
                CameraPosition? cameraPosition = await _cameraPositionRepository.GetById(id);
                if (cameraPosition == null)
                    return ServiceResult<CameraPositionReadDto>.Fail("Camera position not found.");

                _entityToDtoMapper.UpdateEntity(cameraPosition, updateDto);
                await _cameraPositionRepository.Update(cameraPosition);
                CameraPositionReadDto dto = _entityToDtoMapper.ToDto(cameraPosition);
                return ServiceResult<CameraPositionReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<CameraPositionReadDto>.Fail(ex.Message);
            }
        }
    }
}
