using Umuna.Core.Contracts.DTOs.User;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Domain.Mappings;
using Umuna.Server.Infrastructure.Repositories;

namespace Umuna.Server.Infrastructure.Services
{
    public class UserService(
        IUserRepository userRepository,
        IEntityToDtoMapper<User, UserReadDto, UserCreateDto, UserUpdateDto> entityToDtoMapper
        ) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IEntityToDtoMapper<
            User, 
            UserReadDto, 
            UserCreateDto, 
            UserUpdateDto> 
            _entityToDtoMapper = entityToDtoMapper;

        public async Task<ServiceResult<UserReadDto>> Add(UserCreateDto createDto)
        {
            if (createDto == null)
                return ServiceResult<UserReadDto>.Fail("Create DTO is null.");

            if (string.IsNullOrWhiteSpace(createDto.Name) || 
                string.IsNullOrWhiteSpace(createDto.Email) || 
                string.IsNullOrWhiteSpace(createDto.Password))
                return ServiceResult<UserReadDto>.Fail("Name, email and password are required.");

            try
            {
                User user = _entityToDtoMapper.ToEntity(createDto);
                await _userRepository.Add(user);
                UserReadDto dto = _entityToDtoMapper.ToDto(user);
                return ServiceResult<UserReadDto>.Ok(dto);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserReadDto>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult> Delete(int id)
        {
            try
            {
                User? user = await _userRepository.GetById(id);
                if (user == null)
                    return ServiceResult.Fail("User not found.");

                await _userRepository.Delete(id);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<List<UserReadDto>>> GetAll()
        {
            try
            {
                List<User> users = await _userRepository.GetAll();
                List<UserReadDto> dtos = _entityToDtoMapper.ToDtoList(users);
                return ServiceResult<List<UserReadDto>>.Ok(dtos);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<UserReadDto>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<UserReadDto>> GetById(int id)
        {
            // 1. Work with repository using entities
            User? user = await _userRepository.GetById(id);
            if(user == null)
            {
                return ServiceResult<UserReadDto>.Fail("User not found.");
            }

            // 2. Apply business logic
            // (e.g., check permissions, enrich data, etc.)

            // 3. Convert entity → DTO for output
            UserReadDto dto = _entityToDtoMapper.ToDto(user);
            return ServiceResult<UserReadDto>.Ok(dto);
        }

        public async Task<ServiceResult<UserReadDto>> Update(int id, UserUpdateDto updateDto)
        {
            User? user = await _userRepository.GetById(id);
            if (user == null)
            {
                return ServiceResult<UserReadDto>.Fail("User not found.");
            }

            _entityToDtoMapper.UpdateEntity(user, updateDto);

            try
            {
                await _userRepository.Update(user);
                UserReadDto updatedUser = _entityToDtoMapper.ToDto(user);
                return ServiceResult<UserReadDto>.Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserReadDto>.Fail(ex.Message);
            }
        }
    }
}
