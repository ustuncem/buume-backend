using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");
}