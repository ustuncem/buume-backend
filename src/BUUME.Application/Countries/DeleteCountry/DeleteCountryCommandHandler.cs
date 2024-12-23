using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Countries;
using BUUME.SharedKernel;

namespace BUUME.Application.Countries.DeleteCountry;
internal sealed class DeleteCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteCountryCommand, bool>
{
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);
        if (country == null) return Result.Failure<bool>(CountryErrors.NotFound(request.Id)); // CountryErrors.cs has been added to Domain/Countries folder

        country.Delete<Country>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}