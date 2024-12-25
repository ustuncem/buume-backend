using Asp.Versioning;
using BUUME.Application.TaxOffices.CreateTaxOffice;
using BUUME.Application.TaxOffices.DeleteTaxOffice;
using BUUME.Application.TaxOffices.GetAllTaxOffices;
using BUUME.Application.TaxOffices.GetTaxOfficeById;
using BUUME.Application.TaxOffices.UpdateTaxOffice;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaxOfficeResponse = BUUME.Application.TaxOffices.GetTaxOfficeById.TaxOfficeResponse;

namespace BUUME.Api.Controllers.TaxOffices;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/taxoffices")]
[ApiController]
public class TaxOfficesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<TaxOfficeResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<Guid>>> GetAllTaxOffices(
        [FromQuery] GetAllTaxOfficesRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllTaxOfficesQuery(request.searchTerm);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<TaxOfficeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<Guid>>> GetTaxOfficeById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetTaxOfficeByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateTaxOffice(
        [FromBody] CreateTaxOfficeRequest createTaxOfficeRequest, 
        CancellationToken cancellationToken = default)
    {
        var command = new CreateTaxOfficeCommand(createTaxOfficeRequest.name);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateTaxOffice(
        [FromRoute] Guid id,
        [FromBody] UpdateTaxOfficeRequest updateTaxOfficeRequest, 
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateTaxOfficeCommand(id, updateTaxOfficeRequest.name);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteTaxOffice(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteTaxOfficeCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}