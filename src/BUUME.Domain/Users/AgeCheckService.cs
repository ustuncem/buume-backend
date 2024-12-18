namespace BUUME.Domain.Users;

public static class AgeCheckService
{
    private static readonly int MIN_VALID_AGE = 12;
    
    public static bool IsValidAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) 
        {
            age--;
        }

        return age >= MIN_VALID_AGE;
    }
}