namespace BUUME.Application.Abstractions.Sms
{
    public interface ISmsService
    {
        Task SendAsync(string phoneNumber, string message);
    }
}