using System.Text.RegularExpressions;

namespace Domain.Shared;

public static class Utils
{
    public static bool ValidateEmail(string email)
    {
        var parttner = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var regex = new Regex(parttner);
        var result = regex.IsMatch(email);
        return result;
    }
}
