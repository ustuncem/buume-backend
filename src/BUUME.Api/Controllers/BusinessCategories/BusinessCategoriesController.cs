using Asp.Versioning;
using BUUME.Application.BusinessCategories.CreateBusinessCategory;
using BUUME.Application.BusinessCategories.DeleteBusinessCategory;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BUUME.Api.Controllers.BusinessCategories;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/businesscategories")]
[ApiController]
public class BusinessCategoriesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;
    
    // [HttpGet]
    // [ProducesResponseType(typeof(Result<IReadOnlyList<TaxOfficeResponse>>), StatusCodes.Status200OK)]
    // public async Task<ActionResult<Result<Guid>>> GetAllTaxOffices(
    //     CancellationToken cancellationToken = default)
    // {
    //     var query = new GetAllTaxOfficesQuery();
    //     var result = await _sender.Send(query, cancellationToken);
    //     return Ok(result);
    // }
    
    /*[HttpGet("{id:guid}")]
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
    }*/

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateBusinessCategory(
        [FromBody] CreateBusinessCategoryRequest createBusinessCategoryRequest, 
        CancellationToken cancellationToken = default)
    {
        var command = new CreateBusinessCategoryCommand(createBusinessCategoryRequest.name);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
    
    /*[HttpPut("{id:guid}")]
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
    }*/
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteBusinessCategory(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteBusinessCategoryCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}