using Asp.Versioning;
using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Authentication.Register;
using BUUME.Application.Authentication.ValidatePhoneNumber;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.Authentication;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/authentication")]
[ApiController]
public class AuthenticationController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> Register(
        [FromBody] RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new RegisterCommand(request.phoneNumber);
        var result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }
    
    [HttpPost("validatePhoneNumber")]
    [ProducesResponseType(typeof(Result<TokenResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<TokenResponse>>> ValidatePhoneNumber(
        [FromBody] ValidatePhoneNumberRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new ValidatePhoneNumberCommand(request.phoneNumber, request.code);
        var result = await _sender.Send(command, cancellationToken);
        return Ok(result);
    }
}