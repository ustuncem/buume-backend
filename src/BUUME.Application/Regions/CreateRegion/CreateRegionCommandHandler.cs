using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Regions;
using BUUME.SharedKernel;

namespace BUUME.Application.Regions.CreateRegion;

internal sealed class CreateRegionCommandHandler(
    IRegionRepository regionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRegionCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
    {
        var regionName = new Name(request.Name);

        var region = Region.Create(regionName, request.CountryId);

        regionRepository.Add(region);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return region.Id;
    }
}