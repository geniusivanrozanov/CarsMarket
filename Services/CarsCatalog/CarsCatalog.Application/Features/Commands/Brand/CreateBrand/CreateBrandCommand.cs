using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateBrandCommand : IRequest<GetBrandDto>
{
    public CreateBrandCommand(CreateBrandDto createBrandDto)
    {
        CreateBrandDto = createBrandDto;
    }

    public CreateBrandDto CreateBrandDto { get; }
}
