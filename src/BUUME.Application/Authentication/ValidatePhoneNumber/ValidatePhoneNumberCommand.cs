using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Authentication.ValidatePhoneNumber;

public record ValidatePhoneNumberCommand(string PhoneNumber, string Code) : ICommand<TokenResponse>;