using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Users;
using BUUME.SharedKernel;

namespace BUUME.Application.Users.UpdateMe;

internal sealed class UpdateMeCommandHandler(
    IUserRepository userRepository, 
    IUserContext userContext, 
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMeCommand request, CancellationToken cancellationToken)
    {
        var userPhoneNumber = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userPhoneNumber, cancellationToken);

        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);
        
        var firstName = new Name(request.FirstName ?? "");
        var lastName = new Name(request.LastName ?? "");
        var email = new Email(request.Email ?? "");
        var phoneNumber = new PhoneNumber(request.PhoneNumber);
        var birthDate = BirthDateConverter.Convert(request.BirthDate ?? "");
        var gender = request.Gender.HasValue ? (Gender)request.Gender.Value : (Gender?)null;
        
        var updateResult = user.Update(firstName, lastName, email, phoneNumber, birthDate, gender);
        
        if (!updateResult.IsSuccess) return Result.Failure<bool>(updateResult.Error);
        
        userRepository.Update(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}