using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Regions;
using BUUME.SharedKernel;

namespace BUUME.Application.Regions.UpdateRegion;

internal sealed class UpdateRegionCommandHandler(IRegionRepository regionRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRegionCommand, Guid>
{
    private readonly IRegionRepository _regionRepository = regionRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
    {
        var region = await _regionRepository.GetByIdAsync(request.Id, cancellationToken);
        if(region == null) return Result.Failure<Guid>(RegionErrors.NotFound(request.Id));

        var newRegionName = new Name(request.Name);
        var newRegionCountry = request.CountryId;
        region.Update(newRegionName, newRegionCountry);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return region.Id;
    }
}