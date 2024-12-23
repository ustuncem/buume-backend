using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Abstractions.Data;
using BUUME.Domain.TaxOffices;
using BUUME.SharedKernel;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

internal sealed class CreateTaxOfficeCommandHandler(
    ITaxOfficeRepository taxOfficeRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTaxOfficeCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateTaxOfficeCommand request, CancellationToken cancellationToken)
    {
        var taxOfficeId = Guid.NewGuid();
        var taxOfficeName = new Name(request.Name);
        var taxOffice = new TaxOffice(taxOfficeId, taxOfficeName);
        
        taxOfficeRepository.Add(taxOffice);
        await unitOfWork.SaveChangesAsync();
        
        return taxOfficeId;
    }
}