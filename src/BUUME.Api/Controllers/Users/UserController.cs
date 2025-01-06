using Asp.Versioning;
using BUUME.Application.Users.Me;
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
    
    [HttpPost("me")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> Me(
        CancellationToken cancellationToken = default)
    {
        var query = new MeQuery();
        var result = await _sender.Send(query, cancellationToken);
        if (!result.IsSuccess) return BadRequest(new { IsSuccess = result.IsSuccess, Error = result.Error });
        return Ok(result);
    }
}