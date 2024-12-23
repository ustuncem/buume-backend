using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.TaxOffices;
using BUUME.SharedKernel;

namespace BUUME.Application.TaxOffices.UpdateTaxOffice;

internal sealed class UpdateTaxOfficeCommandHandler(ITaxOfficeRepository taxOfficeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTaxOfficeCommand, Guid>
{
    private readonly ITaxOfficeRepository _taxOfficeRepository = taxOfficeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(UpdateTaxOfficeCommand request, CancellationToken cancellationToken)
    {
        var taxOffice = await _taxOfficeRepository.GetByIdAsync(request.Id, cancellationToken);
        if(taxOffice == null) return Result.Failure<Guid>(TaxOfficeErrors.NotFound(request.Id));

        var newTaxOfficeName = new Name(request.Name);
        var updatedTaxOffice = taxOffice.Update(newTaxOfficeName);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return updatedTaxOffice.Id;
    }
}