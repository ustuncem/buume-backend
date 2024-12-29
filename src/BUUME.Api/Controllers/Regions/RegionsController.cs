using Asp.Versioning;
using BUUME.Api.Controllers.Countries;
using BUUME.Application.Regions.CreateRegion;
using BUUME.Application.Regions.DeleteRegion;
using BUUME.Application.Regions.GetRegionById;
using BUUME.Application.Regions.GetRegionsByCountryId;
using BUUME.Application.Regions.UpdateRegion;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegionResponse = BUUME.Application.Regions.GetRegionsByCountryId.RegionResponse;

namespace BUUME.Api.Controllers.Regions;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/regions")]
[ApiController]
public class RegionsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<RegionResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> GetRegionsByCountryId(
        [FromQuery] GetRegionsByCountryIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRegionsByCountryIdQuery(request.countryId, request.searchTerm);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<Application.Regions.GetRegionById.RegionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> GetRegionById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetRegionByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateRegion(
        [FromBody] CreateRegionRequest createRegionRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateRegionCommand(createRegionRequest.name, createRegionRequest.countryId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateRegion(
        [FromRoute] Guid id,
        [FromBody] UpdateRegionRequest updateRegionRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateRegionCommand(id, updateRegionRequest.name, updateRegionRequest.countryId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteRegion(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteRegionCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}