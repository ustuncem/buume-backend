using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Cities;
using BUUME.SharedKernel;

namespace BUUME.Application.Cities.CreateCity;

internal sealed class CreateCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCityCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var cityName = new Name(request.Name);
        var cityCode = new Code(request.Code);
        var city = new City(Guid.NewGuid(), cityName, cityCode, request.CountryId, request.RegionId); 

        cityRepository.Add(city);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return city.Id;
    }
}