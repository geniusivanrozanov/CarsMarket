using System.Text;
using Advertisement.gRPC.Contracts;
using Advertisement.gRPC.Contracts.Models;
using Advertisement.gRPC.Contracts.Requests;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Logging;
using Notification.gRPC.Contracts;
using Notification.gRPC.Contracts.Requests;

namespace FavoriteFilters.Application.Features.Services;

public class FiltersNotificationService : IFiltersNotificationService
{
    private readonly IFilterRepository _filterRepository;
    private readonly IAdvertisementService _advertisementService;
    private readonly INotificationService _notificationService;
    private readonly ILogger<FiltersNotificationService> _logger;

    public FiltersNotificationService(IRepositoryUnitOfWork repositoryUnitOfWork,
        IAdvertisementService advertisementService,
        INotificationService notificationService,
        ILogger<FiltersNotificationService> logger)
    {
        _advertisementService = advertisementService;
        _notificationService = notificationService;
        _logger = logger;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task NotifyUserByFilterId(Guid filterId)
    {
        var filter = await _filterRepository.GetFilterByIdAsync<FilterEntity>(filterId);

        if (filter is null)
        {
            _logger.LogWarning("Filter wit id '{FilterId} doesn't exist'", filterId);
            return;
        }

        var getAdsByQueryParametersRequest = filter.Adapt<GetAdsByQueryParametersRequest>();

        var getAdsByQueryParametersReply =
            await _advertisementService.GetAdsByQueryParametersAsync(getAdsByQueryParametersRequest);

        var ads = getAdsByQueryParametersReply.Ads;
        
        if (ads is null || ads.Count == 0)
        {
            return;
        }

        var message = BuildHtmlPage(ads, filter);

        await _notificationService.SendEmailAsync(new SendEmailRequest
        {
            RecipientEmailAddress = filter.UserEmail,
            Subject = "New ads",
            Body = message
        });
    }

    private string BuildHtmlPage(IEnumerable<AdDataContract> ads, FilterEntity filter)
    {
        var htmlStringBuilder = new StringBuilder();

        foreach (var ad in ads)
        {
            htmlStringBuilder.AppendFormat(
                """
                <div>
                    <h2>{0} {1} {2}</h2>
                    <p>
                        Year: {3} </br>
                        Mileage: {4} </br>
                        Price: {5} {6}</br>
                    </p>
                </div>
                """,
                ad.Brand, ad.Model, ad.Generation, ad.Year, ad.Mileage, ad.Price, ad.Currency.Adapt<string>());
        }

        return htmlStringBuilder.ToString();
    }
}
