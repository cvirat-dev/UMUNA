namespace Umuna.Server.Api.Mappings
{
    public interface IDtoMapper<TDto, TDomainData>
        where TDto : class
        where TDomainData : class
    {
        TDto ToDto(TDomainData domainData);
        TDomainData ToDomainData(TDto dto);
        IEnumerable<TDto> ToDtos(IEnumerable<TDomainData> domainDataList);
        IEnumerable<TDomainData> ToDomainDataList(IEnumerable<TDto> dtoList);
    }
}
