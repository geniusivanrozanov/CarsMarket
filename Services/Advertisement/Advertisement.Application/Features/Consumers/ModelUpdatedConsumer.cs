using Advertisement.Application.Interfaces.Repositories;
using CarsCatalog.Messages.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Consumers;

public class ModelUpdatedConsumer : IConsumer<ModelUpdatedMessage>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<ModelUpdatedConsumer> _logger;

    public ModelUpdatedConsumer(IAdRepository adRepository, ILogger<ModelUpdatedConsumer> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ModelUpdatedMessage> context)
    {
        var modelId = context.Message.ModelId;
        var updatedModelName = context.Message.UpdatedModelName;
        
        await _adRepository.UpdateModelNameAsync(modelId, updatedModelName, context.CancellationToken);
        
        _logger.LogInformation("Model name with id '{ModelId}' has been updated to '{UpdatedModelName}' by {Consumer}",
            modelId,
            updatedModelName,
            nameof(ModelUpdatedConsumer));
    }
}
