using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Regions;
using BUUME.SharedKernel;

namespace BUUME.Application.Regions.DeleteRegion;
internal sealed class DeleteRegionCommandHandler(IRegionRepository regionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRegionCommand, bool>
{
    private readonly IRegionRepository _regionRepository = regionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
    {
        var region = await _regionRepository.GetByIdAsync(request.Id, cancellationToken);
        if (region == null) return Result.Failure<bool>(RegionErrors.NotFound(request.Id));

        region.Delete<Region>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}