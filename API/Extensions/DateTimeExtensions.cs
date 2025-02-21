namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly DateOfBirth){
        var today = DateOnly.FromDateTime(DateTime.Now);
        // Catching the person's age with the today year minus the year of birth
        var age = today.Year - DateOfBirth.Year;

        // As the calculation just by the years can be imprecise (for example, here in february of 2025 I still have 24 years, despite being born in 2000), we have to verify this condition too
        if (DateOfBirth > today.AddYears(-age))
            age--;

        return age;
    }
}
