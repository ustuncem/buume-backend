using System.Text;
using System.Text.Json;
using BUUME.Application.Abstractions.SMS;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BUUME.Infrastructure.SMS;

internal sealed class SmsService(
    HttpClient httpClient, 
    IOptions<SmsOptions> smsOptions,
    ILogger<SmsService> logger) : ISmsService
{
    public async Task SendAsync(string phoneNumber, string message, CancellationToken cancellationToken = default)
    {
        var requestPayload = new
        {
            request = new
            {
                authentication = new
                {
                    key = smsOptions.Value.Key,
                    hash = smsOptions.Value.Hash
                },
                order = new
                {
                    sender = smsOptions.Value.SenderId,
                    iys = smsOptions.Value.Iys,
                    iysList = smsOptions.Value.IysList,
                    message = new
                    {
                        text = message,
                        receipents = new
                        {
                            number = new[] { phoneNumber }
                        }
                    }
                }
            }
        };
        
        var requestJson = JsonSerializer.Serialize(requestPayload);
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        
        try
        {
            var response = await httpClient.PostAsync("", content, cancellationToken);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send message to SMS");
        }
    }
}