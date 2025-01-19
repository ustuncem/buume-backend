using BUUME.Application.Businesses.UpdateBusiness;
using FluentValidation;

namespace BUUME.Application.Businesses.UpdateBusinessContent;

internal sealed class UpdateBusinessContentCommandValidator : AbstractValidator<UpdateBusinessContentCommand>
{
    public UpdateBusinessContentCommandValidator()
    {
        RuleFor(x => x.BusinessId)
            .NotEmpty()
            .WithMessage("Şirket bilgisi zorunludur.");
        
        RuleFor(x => x.FileId)
            .NotEmpty()
            .WithMessage("Dosya bilgisi zorunludur.");
        
        RuleFor(x => x.NewBase64Content)
            .NotEmpty()
            .WithMessage("İçerik girmek zorunludur.");
    }
}