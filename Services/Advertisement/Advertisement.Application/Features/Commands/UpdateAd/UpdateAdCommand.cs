using Advertisement.Application.DTOs.Ad;
using MediatR;

namespace Advertisement.Application.Features.Commands.UpdateAd;

public class UpdateAdCommand : IRequest<GetAdDto>
{
    public UpdateAdCommand(Guid adId, UpdateAdDto updateAdDto)
    {
        AdId = adId;
        UpdateAdDto = updateAdDto;
    }

    public Guid AdId { get; set; }
    public UpdateAdDto UpdateAdDto { get; set; }
}
