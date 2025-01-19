using BUUME.Application.Businesses.UpdateBusinessContent;
using FluentValidation;

namespace BUUME.Application.Businesses.CreateBusinessContent;

internal sealed class CreateBusinessContentCommandValidator : AbstractValidator<CreateBusinessContentCommand>
{
    public CreateBusinessContentCommandValidator()
    {
        RuleFor(x => x.BusinessId)
            .NotEmpty()
            .WithMessage("Şirket bilgisi zorunludur.");
        
        RuleFor(x => x.NewBase64Content)
            .NotEmpty()
            .WithMessage("İçerik girmek zorunludur.");
    }
}