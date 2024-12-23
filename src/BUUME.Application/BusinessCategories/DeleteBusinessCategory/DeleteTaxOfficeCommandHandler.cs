using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.BusinessCategories;
using BUUME.SharedKernel;

namespace BUUME.Application.BusinessCategories.DeleteBusinessCategory;

internal sealed class DeleteBusinessCategoryCommandHandler(IBusinessCategoryRepository businessCategoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteBusinessCategoryCommand, bool>
{
    private readonly IBusinessCategoryRepository _businessCategoryRepository = businessCategoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<bool>> Handle(DeleteBusinessCategoryCommand request, CancellationToken cancellationToken)
    {
        var businessCategory = await _businessCategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if(businessCategory == null) return Result.Failure<bool>(BusinessCategoryErrors.NotFound(request.Id));

        businessCategory.Delete<BusinessCategory>();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}