namespace BUUME.Application.Abstractions.Sms
{
    public interface ISmsService
    {
        Task SendAsync(Domain.Users.PhoneNumber phoneNumber, string message);
    }
}