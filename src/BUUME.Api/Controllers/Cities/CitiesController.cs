using Asp.Versioning;
using BUUME.Application.Cities.CreateCity;
using BUUME.Application.Cities.DeleteCity;
using BUUME.Application.Cities.GetCitiesByCountryId;
using BUUME.Application.Cities.GetCityById;
using BUUME.Application.Cities.UpdateCity;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CityResponse = BUUME.Application.Cities.GetCitiesByCountryId.CityResponse;

namespace BUUME.Api.Controllers.Cities;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/cities")]
[ApiController]
public class CitiesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<CityResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> GetCitiesByCountryId(
        [FromQuery] GetCitiesByCountryIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCitiesByCountryIdQuery(request.countryId, request.searchTerm);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<Application.Cities.GetCityById.CityResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> GetCityById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCityByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateCity(
        [FromBody] CreateCityRequest createCityRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCityCommand(
            createCityRequest.name, 
            createCityRequest.code, 
            createCityRequest.countryId,
            createCityRequest.regionId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateCity(
        [FromRoute] Guid id,
        [FromBody] UpdateCityRequest updateCityRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCityCommand(
            id, 
            updateCityRequest.name,
            updateCityRequest.code,
            updateCityRequest.countryId,
            updateCityRequest.regionId);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteCity(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCityCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}