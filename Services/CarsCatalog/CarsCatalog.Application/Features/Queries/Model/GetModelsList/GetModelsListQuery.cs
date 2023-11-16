using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelsListQuery : IRequest<IEnumerable<GetModelDto>>
{
}
