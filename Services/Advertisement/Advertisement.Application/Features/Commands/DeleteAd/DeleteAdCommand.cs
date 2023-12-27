using MediatR;

namespace Advertisement.Application.Features.Commands.DeleteAd;

public class DeleteAdCommand : IRequest
{
    public DeleteAdCommand(Guid adId)
    {
        AdId = adId;
    }

    public Guid AdId { get; set; }
}
