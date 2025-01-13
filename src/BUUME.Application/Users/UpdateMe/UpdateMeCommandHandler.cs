using BUUME.Application.Abstractions.Authentication;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Files;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Files;
using BUUME.Domain.Users;
using BUUME.SharedKernel;
using File = BUUME.Domain.Files.File;
using Name = BUUME.Domain.Users.Name;

namespace BUUME.Application.Users.UpdateMe;

internal sealed class UpdateMeCommandHandler(
    IUserRepository userRepository, 
    IFileRepository fileRepository,
    IFileUploader fileUploader,
    IUserContext userContext, 
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMeCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateMeCommand request, CancellationToken cancellationToken)
    {
        Guid? fileId = null;
        var userPhoneNumber = userContext.UserId;
        var user = await userRepository.GetByPhoneNumberAsync(userPhoneNumber, cancellationToken);

        if (user == null) return Result.Failure<bool>(UserErrors.NotFound);

        if (!string.IsNullOrWhiteSpace(request.ProfilePhoto))
        {
            var uploadResult = await fileUploader.UploadImageFromBase64Async(request.ProfilePhoto, "profile");
            if (!uploadResult.IsSuccess && !string.IsNullOrWhiteSpace(request.ProfilePhoto)) return Result.Failure<bool>(uploadResult.Error);
        
            var file = File.Create(
                uploadResult.Value.Size, 
                uploadResult.Value.Name, 
                uploadResult.Value.Path,
                uploadResult.Value.Type);
            
            fileRepository.Add(file);
            fileId = file.Id;
        }
        
        
        var firstName = new Name(request.FirstName ?? "");
        var lastName = new Name(request.LastName ?? "");
        var email = new Email(request.Email ?? "");
        var birthDate = BirthDateConverter.Convert(request.BirthDate ?? "");
        var gender = request.Gender.HasValue ? (Gender)request.Gender.Value : (Gender?)null;
        var newFileId = fileId ?? user.ProfilePhotoId;
        
        // TODO: Implement phone number change here.
        var updateResult = user.Update(firstName, lastName, email, user.PhoneNumber, birthDate, gender, newFileId);
        
        if (!updateResult.IsSuccess) return Result.Failure<bool>(updateResult.Error);
        
        userRepository.Update(user);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}