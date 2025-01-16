using BUUME.Application.Abstractions.Data;
using BUUME.Domain.PairDevice.Events;
using BUUME.Domain.Users;
using MediatR;

namespace BUUME.Application.PairDevices.CreatePairDevice;

internal sealed class PairDeviceCreatedDomainEventHandler : INotificationHandler<PairDeviceCreatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PairDeviceCreatedDomainEventHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(PairDeviceCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"PairDeviceCreatedDomainEventHandler = {notification.PairDeviceId}");

        if (notification.HasUserEnabledNotifications)
        {
            var user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);
            if (user == null)
            {
                Console.WriteLine($"User not found: {notification.UserId}");
                return;
            }

            user.ToggleNotificationPermission();

            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
