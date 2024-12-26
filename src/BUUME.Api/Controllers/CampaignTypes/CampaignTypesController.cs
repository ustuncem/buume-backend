using Asp.Versioning;
using BUUME.Application.CampaignTypes.CreateCampaignType;
using BUUME.Application.CampaignTypes.DeleteCampaignType;
using BUUME.Application.CampaignTypes.GetAllCampaignTypes;
using BUUME.Application.CampaignTypes.GetCampaignTypeById;
using BUUME.Application.CampaignTypes.UpdateCampaignType;
using BUUME.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CampaignTypeResponse = BUUME.Application.CampaignTypes.GetAllCampaignTypes.CampaignTypeResponse;
using SingleCampaignTypeResponse = BUUME.Application.CampaignTypes.GetCampaignTypeById.CampaignTypeResponse;

namespace BUUME.Api.Controllers.CampaignTypes;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/campaigntypes")]
[ApiController]
public class CampaignTypesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    [ProducesResponseType(typeof(Result<IReadOnlyList<CampaignTypeResponse>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<Result<IReadOnlyList<CampaignTypeResponse>>>> GetAllCampaignTypes(
        [FromQuery] GetAllCampaignTypesRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCampaignTypesQuery(request.searchTerm);
        var result = await _sender.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(Result<SingleCampaignTypeResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<SingleCampaignTypeResponse>>> GetCampaignTypeById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCampaignTypeByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> CreateCampaignType(
        [FromBody] CreateCampaignTypeRequest createCampaignTypeRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCampaignTypeCommand(
            createCampaignTypeRequest.name, 
            createCampaignTypeRequest.code, 
            createCampaignTypeRequest.description ?? "");

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> UpdateCampaignType(
        [FromRoute] Guid id,
        [FromBody] UpdateCampaignTypeRequest updateCampaignTypeRequest,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCampaignTypeCommand(
            id, 
            updateCampaignTypeRequest.name, 
            updateCampaignTypeRequest.code,
            updateCampaignTypeRequest.description ?? "");

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Result<Guid>>> DeleteCampaignType(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteCampaignTypeCommand(id);

        var result = await _sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(result);
    }
}