using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Districts;
using BUUME.SharedKernel;

namespace BUUME.Application.Districts.CreateDistrict;

internal sealed class CreateDistrictCommandHandler(
    IDistrictRepository districtRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateDistrictCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        var districtName = new Name(request.Name);
        var districtCode = new Code(request.Code);
        var district = new District(Guid.NewGuid(), districtName, districtCode, request.CityId); 

        districtRepository.Add(district);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return district.Id;
    }
}