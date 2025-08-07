using Umuna.ApiServer.Dtos;
using Umuna.Core.Data.Umuna;

namespace Umuna.ApiServer.Mappings.Data
{
    public class PlayerDataMap : IDtoMapper<UserDataDto, UserData>
    {
        public UserData ToDomainData(UserDataDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserData> ToDomainDataList(IEnumerable<UserDataDto> dtoList)
        {
            throw new NotImplementedException();
        }

        public UserDataDto ToDto(UserData domainData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDataDto> ToDtos(IEnumerable<UserData> domainDataList)
        {
            throw new NotImplementedException();
        }
    }
}
