using BUUME.SharedKernel;
using Microsoft.AspNetCore.Diagnostics;

namespace BUUME.Api.Infrastructure;

internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred");

        if (exception is FluentValidation.ValidationException validationException)
        {
            var validationErrors = validationException.Errors.Select(e => e.ErrorCode);
            var validationMessage = string.Join(',', validationErrors);
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationResult = Result.Failure(new(StatusCodes.Status400BadRequest.ToString(), validationMessage,
                ErrorType.Validation));
            await httpContext.Response.WriteAsJsonAsync(validationResult, cancellationToken);

            return true;
        }

        var result = Result.Failure(Error.Failure(StatusCodes.Status500InternalServerError.ToString(), "Internal server error."));
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
