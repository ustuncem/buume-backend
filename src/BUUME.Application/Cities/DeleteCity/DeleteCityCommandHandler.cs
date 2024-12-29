using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Cities;
using BUUME.SharedKernel;

namespace BUUME.Application.Cities.DeleteCity;

internal sealed class DeleteCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCityCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await cityRepository.GetByIdAsync(request.Id, cancellationToken);
        if (city == null) return Result.Failure<bool>(CityErrors.NotFound(request.Id));

        city.Delete<City>();
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}