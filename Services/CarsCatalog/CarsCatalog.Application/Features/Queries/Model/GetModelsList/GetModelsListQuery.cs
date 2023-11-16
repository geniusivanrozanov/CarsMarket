using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelsListQuery : IRequest<IEnumerable<GetModelDto>>
{
    public Guid? BrandId { get; set; }
    public string? BrandName { get; set; }
}
