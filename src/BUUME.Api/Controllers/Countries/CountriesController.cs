using Asp.Versioning;
using BUUME.Application.Countries.CreateCountry;
using BUUME.Application.Countries.DeleteCountry;
using BUUME.Application.Countries.GetAllCountries;
using BUUME.Application.Countries.GetCountryById;
using BUUME.Application.Countries.UpdateCountry;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CountryResponse = BUUME.Application.Countries.GetCountryById.CountryResponse;

namespace BUUME.Api.Controllers.Countries;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/countries")]
[ApiController]
public class CountriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<CountryResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> GetAllCountries(
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCountriesQuery();
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<CountryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> GetCountryById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCountryByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateCountry(
        [FromBody] CreateCountryRequest createCountryRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCountryCommand(createCountryRequest.name, createCountryRequest.code, createCountryRequest.hasRegion);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateCountry(
        [FromRoute] Guid id,
        [FromBody] UpdateCountryRequest updateCountryRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCountryCommand(id, updateCountryRequest.name, updateCountryRequest.code);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteCountry(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCountryCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}