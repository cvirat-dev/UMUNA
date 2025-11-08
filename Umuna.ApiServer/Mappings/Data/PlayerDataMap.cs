using Umuna.ApiServer.DTOs;
using Umuna.Core.Domain.Data;

namespace Umuna.ApiServer.Mappings.Data
{
    public class PlayerDataMap : IDtoMapper<UserDataDto, User>
    {
        public User ToDomainData(UserDataDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ToDomainDataList(IEnumerable<UserDataDto> dtoList)
        {
            throw new NotImplementedException();
        }

        public UserDataDto ToDto(User domainData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDataDto> ToDtos(IEnumerable<User> domainDataList)
        {
            throw new NotImplementedException();
        }
    }
}
