using BUUME.Application.Abstractions.SMS;
using BUUME.Domain.Users;
using BUUME.Domain.Users.Events;
using MediatR;

namespace BUUME.Application.Authentication.Register;

internal sealed class UserRegisteredDomainEventHandler(ISmsService smsService, IUserRepository userRepository)
    : INotificationHandler<UserCreatedDomainEvent>
{

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
         var user = await userRepository.GetByIdAsync(notification.UserId, cancellationToken);
        if (user == null) { return; }
        
         var validationToken = notification.ValidationToken;
        
         await smsService.SendAsync(user.PhoneNumber.Value, $"Doğrulama kodu: {validationToken}", cancellationToken);
    }
}
