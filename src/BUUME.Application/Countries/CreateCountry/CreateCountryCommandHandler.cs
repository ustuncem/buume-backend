using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Data;
using BUUME.Domain.Countries;
using BUUME.SharedKernel;

namespace BUUME.Application.Countries.CreateCountry;

internal sealed class CreateCountryCommandHandler(
    ICountryRepository countryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCountryCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var countryName = new Name(request.Name);
        var countryCode = new Code(request.Code);
        var hasRegion = new HasRegion(request.HasRegion);

        var country = Country.Create(countryName, countryCode, hasRegion);

        countryRepository.Add(country);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return country.Id;
    }
}