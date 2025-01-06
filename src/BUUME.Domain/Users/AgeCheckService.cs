namespace BUUME.Domain.Users;

public static class AgeCheckService
{
    private const int MinValidAge = 12;

    public static bool IsValidAge(DateTime? birthDate)
    {
        if (!birthDate.HasValue) return true;
        
        var today = DateTime.Today;
        int age = today.Year - birthDate.Value.Year;

        if (birthDate.Value.Date > today.AddYears(-age)) 
        {
            age--;
        }

        return age >= MinValidAge;
    }
}