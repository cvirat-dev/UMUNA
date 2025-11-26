using Umuna.Core.Contracts.DTOs;
using Umuna.Server.Domain.Entities;

namespace Umuna.Server.Domain.Mappings
{
    public interface IEntityToDtoMapper <TEntity, TReadDto, TCreateDto, TUpdateDto>
        where TEntity : class, IEntity
        where TReadDto : class, IReadDto
        where TCreateDto : class
        where TUpdateDto : class
    {
        TReadDto ToDto(TEntity entity);
        List<TReadDto> ToDtoList(IEnumerable<TEntity> entities);
        TEntity ToEntity(TReadDto dto);
        TEntity ToEntity(TCreateDto dto);
        void UpdateEntity(TEntity entity, TUpdateDto dto);
    }
}
