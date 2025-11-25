using Umuna.Server.Domain.Entities;
using Umuna.Core.Contracts.DTOs.User;

namespace Umuna.ApiServer.Mappings.Data
{
    public class PlayerDataMap : IDtoMapper<UserCreateDto, User>
    {
        public User ToDomainData(UserCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ToDomainDataList(IEnumerable<UserCreateDto> dtoList)
        {
            throw new NotImplementedException();
        }

        public UserCreateDto ToDto(User domainData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserCreateDto> ToDtos(IEnumerable<User> domainDataList)
        {
            throw new NotImplementedException();
        }
    }
}
