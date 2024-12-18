using BUUME.SharedKernel;

namespace BUUME.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found", ErrorType.NotFound);
    
    public static Error NotInValidAge = new("User.NotInValidAge", "The user is in the wrong age", ErrorType.Failure);
}