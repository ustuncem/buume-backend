using BUUME.Application.Abstractions.Sms;

namespace BUUME.Infrastructure.Sms
{
    internal sealed class SmsService : ISmsService
    {
        public Task SendAsync(Domain.Users.PhoneNumber phoneNumber, string message)
        {
            return Task.CompletedTask;
        }
    }
}