using BUUME.Domain.PairDevice.Events;
using MediatR;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class PairDeviceCreatedDomainEventHandler : INotificationHandler<PairDeviceCreatedDomainEvent>
{

    public Task Handle(PairDeviceCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"PairDeviceCreatedDomainEventHandler = {notification.PairDeviceId}");
        return Task.CompletedTask;
    }
}
