using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.CampaignTypes;
using BUUME.SharedKernel;

namespace BUUME.Application.CampaignTypes.UpdateCampaignType;

internal sealed class UpdateCampaignTypeCommandHandler(ICampaignTypeRepository campaignTypeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCampaignTypeCommand, Guid>
{
    private readonly ICampaignTypeRepository _campaignTypeRepository = campaignTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(UpdateCampaignTypeCommand request, CancellationToken cancellationToken)
    {
        var campaignType = await _campaignTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        if(campaignType == null) return Result.Failure<Guid>(CampaignTypeErrors.NotFound(request.Id));

        var newCampaignTypeName = new Name(request.Name);
        var newCampaignTypeCode = Code.GetFromCode(request.Code);
        var newCampaignTypeDescription = new Description(request.Description);
        
        campaignType.Update(newCampaignTypeName, newCampaignTypeDescription, newCampaignTypeCode);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return campaignType.Id;
    }
}