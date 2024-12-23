using FluentValidation;

namespace BUUME.Application.BusinessCategories.UpdateBusinessCategory;

internal sealed class UpdateBusinessCategoryCommandValidator : AbstractValidator<UpdateBusinessCategoryCommand>
{
    public UpdateBusinessCategoryCommandValidator()
    {
        RuleFor(to => to.Name).NotEmpty().Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(BusinessCategoryErrorCodes.CreateBusinessCategory.InvalidName);
    }
}