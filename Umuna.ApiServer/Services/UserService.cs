using Microsoft.EntityFrameworkCore;
using Umuna.ApiServer.Data;
using Umuna.ApiServer.DTOs;
using Umuna.Core.Domain.Data;
using Umuna.Core.Models;

namespace Umuna.ApiServer.Services
{
    public class UserService(AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<ServiceResult<UserDto>> AddUserAsync(UserDto dto)
        {
            foreach (var (value, message) in new (string Value, string Message)[]
            {
                (dto.Name, "Username cannot be empty."),
                (dto.Email, "Email cannot be empty."),
                (dto.Password, "Password cannot be empty.")
            })
            {
                if (string.IsNullOrWhiteSpace(value))
                    return ServiceResult<UserDto>.Fail(message);
            }

            // Check if user exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (existingUser != null)
            {
                return ServiceResult<UserDto>.Fail("User with this email already exists");
            }

            // Map DTO to Entity
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Map Entity back to DTO
            var resultDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };

            return ServiceResult<UserDto>.Ok(resultDto);
        }

        public Task<ServiceResult<UserDto>> GetUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                return Task.FromResult(ServiceResult<UserDto>.Fail($"Invalid user ID: {id}."));
            }

            User? user = _context.Users.Find(id);
            if (user == null)
            {
                return Task.FromResult(ServiceResult<UserDto>.Fail($"User with ID {id} not found."));
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
            return Task.FromResult(ServiceResult<UserDto>.Ok(userDto));
        }
    }
}
