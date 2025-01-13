using Asp.Versioning;
using BUUME.Application.Businesses.CreateBusiness;
using BUUME.Application.Businesses.GetBusinessForCurrentUser;
using BUUME.Application.Businesses.GetBusinessHeaderForCurrentUser;
using BUUME.Application.Businesses.UpdateBusiness;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.Businesses;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/businesses")]
[ApiController]
[Authorize]
public class BusinessController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateBusiness(
        [FromBody] CreateBusinessCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    [HttpPost("mine")]
    [ProducesResponseType(typeof(Result<BusinessResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<BusinessResponse>>> GetBusinessForCurrentUser(
        CancellationToken cancellationToken = default)
    {
        var query = new GetBusinessForCurrentUserQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    [HttpGet("header")]
    [ProducesResponseType(typeof(Result<BusinessResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<BusinessResponse>>> GetBusinessHeaderForCurrentUser(
        CancellationToken cancellationToken = default)
    {
        var query = new GetBusinessHeaderForCurrentUserQuery();
        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateBusiness(
        [FromBody] UpdateBusinessCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}