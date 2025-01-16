using Asp.Versioning;
using BUUME.Application.PairDevices.CreatePairDevice;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.PairDevice;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/pairdevice")]
[ApiController]
[Authorize]
public class PairDeviceController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreatePairDevice(
        [FromBody] CreatePairDeviceCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}