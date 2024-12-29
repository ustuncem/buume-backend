using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Cities.DeleteCity;
using BUUME.Domain.Cities;
using BUUME.Domain.Districts;
using BUUME.SharedKernel;

namespace BUUME.Application.Districts.DeleteDistrict;

internal sealed class DeleteDistrictCommandHandler(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteDistrictCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        var district = await districtRepository.GetByIdAsync(request.Id, cancellationToken);
        if (district == null) return Result.Failure<bool>(DistrictErrors.NotFound(request.Id));

        district.Delete<District>();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}