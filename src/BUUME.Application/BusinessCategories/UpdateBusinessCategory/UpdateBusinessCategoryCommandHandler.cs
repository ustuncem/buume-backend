using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.BusinessCategories;
using BUUME.SharedKernel;

namespace BUUME.Application.BusinessCategories.UpdateBusinessCategory;

internal sealed class UpdateBusinessCategoryCommandHandler(IBusinessCategoryRepository businessCategoryRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateBusinessCategoryCommand, Guid>
{
    private readonly IBusinessCategoryRepository _businessCategoryRepository = businessCategoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<Guid>> Handle(UpdateBusinessCategoryCommand request, CancellationToken cancellationToken)
    {
        var businessCategory = await _businessCategoryRepository.GetByIdAsync(request.Id, cancellationToken);
        if(businessCategory == null) return Result.Failure<Guid>(BusinessCategoryErrors.NotFound(request.Id));

        var newBusinessCategoryName = new Name(request.Name);
        var updatedBusinessCategory = businessCategory.Update(newBusinessCategoryName);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return updatedBusinessCategory.Id;
    }
}