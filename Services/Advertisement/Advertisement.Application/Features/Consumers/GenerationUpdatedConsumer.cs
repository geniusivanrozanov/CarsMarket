using Advertisement.Application.Interfaces.Repositories;
using CarsCatalog.Messages.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Consumers;

public class GenerationUpdatedConsumer : IConsumer<GenerationUpdatedMessage>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<GenerationUpdatedConsumer> _logger;

    public GenerationUpdatedConsumer(IAdRepository adRepository, ILogger<GenerationUpdatedConsumer> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<GenerationUpdatedMessage> context)
    {
        var generationId = context.Message.GenerationId;
        var updatedGenerationName = context.Message.UpdatedGenerationName;
        
        await _adRepository.UpdateGenerationNameAsync(generationId, updatedGenerationName, context.CancellationToken);
        
        _logger.LogInformation(
            "Generation name with id '{GenerationId}' has been updated to '{UpdatedGenerationName}' by {Consumer}",
            generationId,
            updatedGenerationName,
            nameof(GenerationUpdatedConsumer));
    }
}
