using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationByIdQuery : IRequest<GetGenerationDto>
{
    private readonly Guid _generationId;

    public GetGenerationByIdQuery(Guid generationId)
    {
        _generationId = generationId;
    }

    public Guid GenerationId => _generationId;
}
