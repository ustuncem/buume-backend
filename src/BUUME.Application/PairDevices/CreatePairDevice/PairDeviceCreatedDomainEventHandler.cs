using BUUME.Application.Abstractions.Data;
using BUUME.Domain.PairDevice.Events;
using BUUME.Domain.Users;
using MediatR;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class PairDeviceCreatedDomainEventHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : INotificationHandler<PairDeviceCreatedDomainEvent>
{
    public async Task Handle(PairDeviceCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user == null) return;
        
        user.ToggleNotificationPermission(notification.HasUserEnabledNotifications);
        userRepository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
