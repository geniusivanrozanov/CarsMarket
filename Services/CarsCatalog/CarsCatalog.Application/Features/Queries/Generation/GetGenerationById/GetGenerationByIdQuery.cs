using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationByIdQuery(Guid generationId) : IRequest<GetGenerationDto>
{
    public Guid GenerationId => generationId;
}
