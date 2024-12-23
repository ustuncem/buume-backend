using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Countries;
using BUUME.SharedKernel;

namespace BUUME.Application.Countries.UpdateCountry;

internal sealed class UpdateCountryCommandHandler(ICountryRepository countryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCountryCommand, Guid>
{
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepository.GetByIdAsync(request.Id, cancellationToken);
        if(country == null) return Result.Failure<Guid>(CountryErrors.NotFound(request.Id));

        var newCountryName = new Name(request.Name);
        var newCountryCode = new Code(request.Code);
        country.Update(newCountryName, newCountryCode);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return country.Id;
    }
}