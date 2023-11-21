using Advertisement.Application.DTOs.Ad;
using MediatR;

namespace Advertisement.Application.Features.Commands.CreateAd;

public class CreateAdCommand : IRequest<GetAdDto>
{
    public CreateAdCommand(CreateAdDto createAdDto)
    {
        CreateAdDto = createAdDto;
    }

    public CreateAdDto CreateAdDto { get; set; }
}
