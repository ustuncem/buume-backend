using BUUME.Application.Abstractions.Sms;

namespace BUUME.Infrastructure.Sms
{
    internal sealed class SmsService : ISmsService
    {
        public Task SendAsync(string phoneNumber, string message)
        {
            return Task.CompletedTask;
        }
    }
}