using Asp.Versioning;
using BUUME.Application.Users.DeleteMe;
using BUUME.Application.Users.GetMeHeader;
using BUUME.Application.Users.HasBusiness;
using BUUME.Application.Users.HasUserSwitchedToBusiness;
using BUUME.Application.Users.Me;
using BUUME.Application.Users.ToggleBusinessSwitch;
using BUUME.Application.Users.UpdateMe;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.Users;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/users")]
[ApiController]
[Authorize]
public class UsersController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpGet("me")]
    [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserResponse>>> Me(
        CancellationToken cancellationToken = default)
    {
        var query = new MeQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpGet("meHeader")]
    [ProducesResponseType(typeof(Result<MeHeaderResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<MeHeaderResponse>>> MeHeader(
        CancellationToken cancellationToken = default)
    {
        var query = new GetMeHeaderQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpPut("updateMe")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserResponse>>> UpdateMe(
        [FromBody] UpdateMeCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpDelete("deleteMe")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<UserResponse>>> DeleteMe(
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteMeCommand();
        var result = await _sender.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpGet("hasBusiness")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> HasBusiness(
        CancellationToken cancellationToken = default)
    {
        var query = new HasBusinessQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpGet("hasUserSwitchedToBusiness")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> HasUserSwitchedToBusiness(
        CancellationToken cancellationToken = default)
    {
        var query = new HasUserSwitchedToBusinessQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
    
    [HttpPut("toggleBusinessSwitch")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<bool>>> ToggleBusinessSwitch(
        CancellationToken cancellationToken = default)
    {
        var command = new ToggleBusinessSwitchCommand();
        var result = await _sender.Send(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
}