using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelByIdQuery(Guid modelId) : IRequest<GetModelDto>
{
    public Guid ModelId => modelId;
}
