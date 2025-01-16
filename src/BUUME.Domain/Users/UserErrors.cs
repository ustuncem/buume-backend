using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "The user with the specified identifier was not found", ErrorType.NotFound);
    
    public static readonly Error NotInValidAge = new("User.NotInValidAge", "The user is in the wrong age", ErrorType.Failure);
    public static readonly Error NoBusinessFound = new("User.NoBusinessFound", "The user has no business", ErrorType.Failure);
    public static readonly Error UnknownError = new("User.UnknownError", "Unknown Error", ErrorType.Failure);
}