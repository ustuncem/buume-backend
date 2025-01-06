using System.Globalization;

namespace BUUME.Application.Users.UpdateMe;

internal static class BirthDateConverter
{
    public static DateTime? Convert(string birthDate)
    {
        if (string.IsNullOrWhiteSpace(birthDate)) return null;
        
        DateTime localDateTime = DateTime.ParseExact(
            birthDate,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal
        );

        DateTime utcDateTime = localDateTime.ToUniversalTime();
        
        return utcDateTime;
    }
}