namespace BUUME.Application.Abstractions.SMS;

public interface ISmsService
{
    public Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default);
}