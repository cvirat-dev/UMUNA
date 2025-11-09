using Umuna.ApiServer.DTOs;
using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.Mappings.Data
{
    public class PlayerDataMap : IDtoMapper<UserDto, User>
    {
        public User ToDomainData(UserDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ToDomainDataList(IEnumerable<UserDto> dtoList)
        {
            throw new NotImplementedException();
        }

        public UserDto ToDto(User domainData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> ToDtos(IEnumerable<User> domainDataList)
        {
            throw new NotImplementedException();
        }
    }
}
