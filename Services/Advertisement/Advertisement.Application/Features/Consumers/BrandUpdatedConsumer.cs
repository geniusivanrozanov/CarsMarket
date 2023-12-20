using Advertisement.Application.Interfaces.Repositories;
using CarsCatalog.Messages.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Advertisement.Application.Features.Consumers;

public class BrandUpdatedConsumer : IConsumer<BrandUpdatedMessage>
{
    private readonly IAdRepository _adRepository;
    private readonly ILogger<BrandUpdatedConsumer> _logger;

    public BrandUpdatedConsumer(IAdRepository adRepository, ILogger<BrandUpdatedConsumer> logger)
    {
        _adRepository = adRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BrandUpdatedMessage> context)
    {
        var brandId = context.Message.BrandId;
        var updatedBrandName = context.Message.UpdatedBrandName;
        
        await _adRepository.UpdateBrandNameAsync(brandId, updatedBrandName, context.CancellationToken);
        
        _logger.LogInformation("Brand name with id '{BrandId}' has been updated to '{UpdatedBrandName}' by {Consumer}",
            brandId,
            updatedBrandName,
            nameof(BrandUpdatedConsumer));
    }
}
