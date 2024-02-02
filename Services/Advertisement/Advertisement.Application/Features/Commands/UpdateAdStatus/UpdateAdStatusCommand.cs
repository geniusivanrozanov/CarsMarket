using Advertisement.Application.DTOs.Ad;
using MediatR;

namespace Advertisement.Application.Features.Commands.UpdateAdStatus;

public class UpdateAdStatusCommand : IRequest<GetAdDto>
{
    public UpdateAdStatusCommand(Guid adId, UpdateAdStatusDto updateAdStatusDto)
    {
        AdId = adId;
        UpdateAdStatusDto = updateAdStatusDto;
    }

    public Guid AdId { get; }
    public UpdateAdStatusDto UpdateAdStatusDto { get; }
}
