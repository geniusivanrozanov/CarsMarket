using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateBrandCommand(CreateBrandDto createBrandDto) : IRequest<GetBrandDto>
{
    public CreateBrandDto CreateBrandDto { get; } = createBrandDto;
}
