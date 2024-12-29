using BUUME.Domain.Countries.Events;
using MediatR;

namespace BUUME.Application.Countries.CreateCountry;

internal sealed class CountryCreatedDomainEventHandler : INotificationHandler<CountryCreatedDomainEvent>
{

    public Task Handle(CountryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"CountryCreatedDomainEventHandler = {notification.CountryId}");
        return Task.CompletedTask;
    }
}
