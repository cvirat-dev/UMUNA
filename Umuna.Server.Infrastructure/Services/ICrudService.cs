using Umuna.Core.Contracts.DTOs;
using Umuna.Core.Contracts.Models;

namespace Umuna.Server.Infrastructure.Services
{
    public interface ICrudService<TCreateDto, TUpdateDto, TReadDto> 
        where TCreateDto : class
        where TUpdateDto : class
        where TReadDto : class, IReadDto
    {
        Task<ServiceResult<TReadDto>> GetById(string id);
        Task<ServiceResult<List<TReadDto>>> GetAll();
        Task<ServiceResult<TReadDto>> Add(TCreateDto createDto);
        Task<ServiceResult<TReadDto>> Update(string id, TUpdateDto updateDto);
        Task<ServiceResult> Delete(string id);
    }
}
