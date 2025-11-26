using Umuna.Core.Contracts.DTOs.UserSettings;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class UserSettingsService(
        IUserSettingsRepository userSettingsRepository,
        IEntityToDtoMapper<UserSettings, SettingsReadDto, SettingsCreateDto, SettingsUpdateDto> entityToDtoMapper
        ) : IUserSettingsService
    {
        private readonly IUserSettingsRepository _userSettingsRepository = userSettingsRepository;
        private readonly IEntityToDtoMapper<
            UserSettings, 
            SettingsReadDto, 
            SettingsCreateDto, 
            SettingsUpdateDto> 
            _entityToDtoMapper = entityToDtoMapper;

        public async Task<ServiceResult<SettingsReadDto>> Add(SettingsCreateDto createDto)
        {
            if (createDto == null)
                return ServiceResult<SettingsReadDto>.Fail("Create DTO is null.");

            if (createDto.UserId <= 0)
                return ServiceResult<SettingsReadDto>.Fail("Valid UserId is required.");

            try
            {
                UserSettings settings = _entityToDtoMapper.ToEntity(createDto);
                await _userSettingsRepository.Add(settings);
                SettingsReadDto dto = _entityToDtoMapper.ToDto(settings);
                return ServiceResult<SettingsReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SettingsReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            try
            {
                UserSettings? settings = await _userSettingsRepository.GetById(id);
                if (settings == null)
                    return ServiceResult.Fail("Settings not found.");

                await _userSettingsRepository.Delete(id);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<SettingsReadDto>>> GetAll()
        {
            try
            {
                List<UserSettings> settingsList = await _userSettingsRepository.GetAll();
                List<SettingsReadDto> dtos = [];

                foreach (UserSettings settings in settingsList)
                {
                    dtos.Add(_entityToDtoMapper.ToDto(settings));
                }
                return ServiceResult<List<SettingsReadDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<SettingsReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<SettingsReadDto>> GetById(int id)
        {
            try
            {
                UserSettings? settings = await _userSettingsRepository.GetById(id);
                if (settings == null)
                    return ServiceResult<SettingsReadDto>.Fail("Settings not found.");

                SettingsReadDto dto = _entityToDtoMapper.ToDto(settings);
                return ServiceResult<SettingsReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SettingsReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<SettingsReadDto>> GetByUserId(int id)
        {
            try
            {
                UserSettings? settings = await _userSettingsRepository.GetByUserIdAsync(id);
                if (settings == null)
                    return ServiceResult<SettingsReadDto>.Fail("Settings not found for user.");

                SettingsReadDto dto = _entityToDtoMapper.ToDto(settings);
                return ServiceResult<SettingsReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SettingsReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<SettingsReadDto>> Update(int id, SettingsUpdateDto updateDto)
        {
            if (updateDto == null)
                return ServiceResult<SettingsReadDto>.Fail("Update DTO is null.");

            try
            {
                UserSettings? settings = await _userSettingsRepository.GetById(id);
                if (settings == null)
                    return ServiceResult<SettingsReadDto>.Fail("Settings not found.");

                settings.CameraRotationSpeed = updateDto.CameraRotationSpeed;
                settings.CameraTranslationSpeed = updateDto.CameraTranslationSpeed;
                settings.CameraZoomSpeed = updateDto.CameraZoomSpeed;

                await _userSettingsRepository.Update(settings);

                SettingsReadDto dto = _entityToDtoMapper.ToDto(settings);
                return ServiceResult<SettingsReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<SettingsReadDto>.Fail(ex.Message);
            }
        }
    }
}
