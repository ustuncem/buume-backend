using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.CampaignTypes;
using BUUME.SharedKernel;

namespace BUUME.Application.CampaignTypes.CreateCampaignType;

internal sealed class CreateCampaignTypeCommandHandler(
    ICampaignTypeRepository campaignTypeRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCampaignTypeCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateCampaignTypeCommand request, CancellationToken cancellationToken)
    {
        var campaignTypeName = new Name(request.Name);
        var campaignTypeCode = Code.GetFromCode(request.Code);
        var campaignTypeDescription = new Description(request.Description);

        var campaignType = CampaignType.Create(campaignTypeName, campaignTypeDescription, campaignTypeCode);

        campaignTypeRepository.Add(campaignType);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return campaignType.Id;
    }
}