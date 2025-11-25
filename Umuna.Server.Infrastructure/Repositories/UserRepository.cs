using Microsoft.EntityFrameworkCore;
using Umuna.Core.Contracts.Models;
using Umuna.Server.Domain.Entities;
using Umuna.Server.Infrastructure.Database;

namespace Umuna.Server.Infrastructure.Repositories
{
    public class UserRepository(UmunaDbContext context) : IUserRepository
    {
        private readonly UmunaDbContext _context = context;

        public async Task<ServiceResult> AddAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        Task<ServiceResult> IUserRepository.DeleteAsync(User user)
        {
            try
            {
                _context.Users.Remove(user);
                _context.SaveChangesAsync();
                return Task.FromResult(ServiceResult.Ok());

            }
            catch (Exception ex)
            {
                return Task.FromResult(ServiceResult.Fail(ex.Message));
            }
        }

        Task<ServiceResult<List<User>>> IUserRepository.GetAllAsync()
        {
            try
            {
                var users = _context.Users
                                .ToListAsync();
                return Task.FromResult(ServiceResult<List<User>>.Ok(users.Result));
            }
            catch (Exception ex)
            {
                return Task.FromResult(ServiceResult<List<User>>.Fail(ex.Message));
            }
        }

        Task<ServiceResult<User>> IUserRepository.GetByIdAsync(int id)
        {
            try
            {
                var user = _context.Users
                               .FirstOrDefaultAsync(u => u.Id == id);
                if (user.Result != null)
                {
                    return Task.FromResult(ServiceResult<User>.Ok(user.Result));
                }
                else
                {
                    return Task.FromResult(ServiceResult<User>.Fail("User not found"));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult(ServiceResult<User>.Fail(ex.Message));
            }
        }

        Task<ServiceResult> IUserRepository.UpdateAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChangesAsync();
                return Task.FromResult(ServiceResult.Ok());
            }
            catch (Exception ex)
            {
                return Task.FromResult(ServiceResult.Fail(ex.Message));
            }
        }
    }
}
