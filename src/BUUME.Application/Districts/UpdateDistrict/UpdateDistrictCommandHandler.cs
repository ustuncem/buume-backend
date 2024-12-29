using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Districts;
using BUUME.SharedKernel;

namespace BUUME.Application.Districts.UpdateDistrict;

internal sealed class UpdateDistrictCommandHandler(IDistrictRepository districtRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateDistrictCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        var district = await districtRepository.GetByIdAsync(request.Id, cancellationToken);
        if(district == null) return Result.Failure<Guid>(DistrictErrors.NotFound(request.Id));

        var newDistrictName = new Name(request.Name);
        var newDistrictCode = new Code(request.Code);
        district.Update(newDistrictName, newDistrictCode, request.CityId);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return district.Id;
    }
}