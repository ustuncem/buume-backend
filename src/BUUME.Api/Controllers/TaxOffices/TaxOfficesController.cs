using Asp.Versioning;
using BUUME.Application.TaxOffices.CreateTaxOffice;
using BUUME.Application.TaxOffices.DeleteTaxOffice;
using BUUME.Application.TaxOffices.UpdateTaxOffice;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.TaxOffices;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/taxoffices")]
[ApiController]
public class TaxOfficesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

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