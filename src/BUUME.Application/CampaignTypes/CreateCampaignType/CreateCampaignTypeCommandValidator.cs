using FluentValidation;

namespace BUUME.Application.CampaignTypes.CreateCampaignType;

internal sealed class CreateCampaignTypeCommandValidator : AbstractValidator<CreateCampaignTypeCommand>
{
    public CreateCampaignTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode(CampaignTypeErrorCodes.CreateCampaignType.MissingName)
            .Matches("^[a-zA-ZÇçĞğİıÖöŞşÜü]+( [a-zA-ZÇçĞğİıÖöŞşÜü]+)*$")
            .WithErrorCode(CampaignTypeErrorCodes.CreateCampaignType.InvalidName);
        
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithErrorCode(CampaignTypeErrorCodes.CreateCampaignType.MissingCode)
            .MaximumLength(80)
            .WithErrorCode(CampaignTypeErrorCodes.CreateCampaignType.MaxLengthCode);

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithErrorCode(CampaignTypeErrorCodes.CreateCampaignType.MaxLengthDescription);
    }
}