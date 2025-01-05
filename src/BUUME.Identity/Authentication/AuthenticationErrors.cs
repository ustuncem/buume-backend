using BUUME.SharedKernel;

namespace BUUME.Identity.Authentication;

public static class AuthenticationErrors
{
    public static Error UserAlreadyExist(string phoneNumber) => Error.Failure(
        "Authentication.UserAlreadyExist", 
        $"User with {phoneNumber} already exists.");
    
    public static Error UnknownError() => Error.Failure(
        "Authentication.UnknownError", 
        $"Unknown error occured.");
    
    public static Error NotFound(Guid userId) => Error.NotFound(
        "Authentication.NotFound",
        $"The business category with the Id = '{userId}' was not found.");
    
    public static Error UnableToUpdate(Guid userId) => Error.Failure(
        "Authentication.UnableToUpdate",
        $"The user with the Id = '{userId}' was not updated.");
    
    public static Error NotFound(string phoneNumber) => Error.NotFound(
        "Authentication.NotFound",
        $"The business category with the Id = '{phoneNumber}' was not found");
}
