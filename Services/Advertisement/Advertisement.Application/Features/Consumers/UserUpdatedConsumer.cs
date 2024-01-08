using Advertisement.Application.Interfaces.Repositories;
using Identity.Messages.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Consumers;

public class UserUpdatedConsumer : IConsumer<UserUpdatedMessage>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<UserUpdatedConsumer> _logger;

    public UserUpdatedConsumer(IAdRepository adRepository, ILogger<UserUpdatedConsumer> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserUpdatedMessage> context)
    {
        var ownerId = context.Message.UserId;
        var updatedOwnerName = context.Message.UpdatedFirstName;
        
        await _adRepository.UpdateOwnerNameAsync(ownerId, updatedOwnerName, context.CancellationToken);
        
        _logger.LogInformation("Owner name with id '{OwnerId}' has been updated to '{UpdatedOwnerName}' by {Consumer}",
            ownerId,
            updatedOwnerName,
            nameof(UserUpdatedConsumer));
    }
}
