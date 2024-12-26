using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.CampaignTypes;
using BUUME.SharedKernel;

namespace BUUME.Application.CampaignTypes.DeleteCampaignType;

internal sealed class DeleteCampaignTypeCommandHandler(ICampaignTypeRepository campaignTypeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCampaignTypeCommand, bool>
{
    private readonly ICampaignTypeRepository _campaignTypeRepository = campaignTypeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteCampaignTypeCommand request, CancellationToken cancellationToken)
    {
        var campaignType = await _campaignTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        if (campaignType == null) return Result.Failure<bool>(CampaignTypeErrors.NotFound(request.Id));

        campaignType.Delete<CampaignType>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}