using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.BusinessCategories;
using BUUME.Domain.TaxOffices;
using BUUME.SharedKernel;
using Name = BUUME.Domain.BusinessCategories.Name;

namespace BUUME.Application.BusinessCategories.CreateBusinessCategory;

internal sealed class CreateBusinessCategoryCommandHandler(
    IBusinessCategoryRepository businessCategoryRepository, 
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBusinessCategoryCommand, Guid>
{

    public async Task<Result<Guid>> Handle(CreateBusinessCategoryCommand request, CancellationToken cancellationToken)
    {
        var businessCategoryId = Guid.NewGuid();
        var businessCategoryName = new Name(request.Name);
        var businessCategory = new BusinessCategory(businessCategoryId, businessCategoryName);
        
        businessCategoryRepository.Add(businessCategory);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return businessCategoryId;
    }
}