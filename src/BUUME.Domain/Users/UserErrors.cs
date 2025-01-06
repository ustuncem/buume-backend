using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found", ErrorType.NotFound);
    
    public static readonly Error NotInValidAge = new("User.NotInValidAge", "The user is in the wrong age", ErrorType.Failure);
}