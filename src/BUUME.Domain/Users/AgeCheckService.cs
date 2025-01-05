namespace BUUME.Domain.Users;

public static class AgeCheckService
{
    private const int MinValidAge = 12;

    public static bool IsValidAge(DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age)) 
        {
            age--;
        }

        return age >= MinValidAge;
    }
}