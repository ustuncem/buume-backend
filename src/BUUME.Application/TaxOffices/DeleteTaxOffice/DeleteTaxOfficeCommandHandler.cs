using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.TaxOffices;
using BUUME.SharedKernel;

namespace BUUME.Application.TaxOffices.DeleteTaxOffice;

internal sealed class DeleteTaxOfficeCommandHandler(ITaxOfficeRepository taxOfficeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteTaxOfficeCommand, bool>
{
    private readonly ITaxOfficeRepository _taxOfficeRepository = taxOfficeRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteTaxOfficeCommand request, CancellationToken cancellationToken)
    {
        var taxOffice = await _taxOfficeRepository.GetByIdAsync(request.Id, cancellationToken);
        if(taxOffice == null) return Result.Failure<bool>(TaxOfficeErrors.NotFound(request.Id));

        taxOffice.Delete<TaxOffice>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}