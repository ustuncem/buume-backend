using Asp.Versioning;
using BUUME.Application.Districts.CreateDistrict;
using BUUME.Application.Districts.DeleteDistrict;
using BUUME.Application.Districts.GetDistrictById;
using BUUME.Application.Districts.GetDistrictsByCityId;
using BUUME.Application.Districts.UpdateDistrict;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DistrictResponse = BUUME.Application.Districts.GetDistrictsByCityId.DistrictResponse;

namespace BUUME.Api.Controllers.Districts;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/districts")]
[ApiController]
public class DistrictsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<DistrictResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> GetDistrictsByCityId(
        [FromQuery] GetDistrictsByCityIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDistrictsByCityIdQuery(request.cityId, request.searchTerm);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<Application.Districts.GetDistrictById.DistrictResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> GetDistrictById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetDistrictByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateDistrict(
        [FromBody] CreateDistrictRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateDistrictCommand(
            request.name, 
            request.code, 
            request.cityId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateDistrict(
        [FromRoute] Guid id,
        [FromBody] UpdateDistrictRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateDistrictCommand(
            id, 
            request.name,
            request.code,
            request.cityId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteDistrict(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteDistrictCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}