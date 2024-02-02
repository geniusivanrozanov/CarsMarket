using Advertisement.gRPC.Contracts.Requests;
using FavoriteFilters.Domain.Entities;
using Mapster;

namespace FavoriteFilters.Application.Mappers;

public class FilterMapperConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<FilterEntity, GetAdsByQueryParametersRequest>()
            .Map(d => d.MinCreatedAt, s => s.LastExecutedAt.DateTime);
    }
}
