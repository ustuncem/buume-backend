using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Cities;
using BUUME.SharedKernel;

namespace BUUME.Application.Cities.UpdateCity;

internal sealed class UpdateCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCityCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(request.Id, cancellationToken);
        if(city == null) return Result.Failure<Guid>(CityErrors.NotFound(request.Id));

        var newCityName = new Name(request.Name);
        var newCityCode = new Code(request.Code);
        city.Update(newCityName, newCityCode, request.CountryId, request.RegionId);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return city.Id;
    }
}