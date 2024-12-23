using FluentValidation;

namespace BUUME.Application.BusinessCategories.CreateBusinessCategory;

internal sealed class CreateBusinessCategoryCommandValidator : AbstractValidator<CreateBusinessCategoryCommand>
{
    public CreateBusinessCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(BusinessCategoryErrorCodes.CreateBusinessCategory.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(BusinessCategoryErrorCodes.CreateBusinessCategory.InvalidName);
    }
}