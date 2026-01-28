using System.Globalization;

namespace BaseProject.Models.Extensions;

public static class StringExtension
{
    public static string NormalizeText(this string str)
        => str.Trim().ToUpperInvariant();

    public static bool IsNullOrEmpty(this string? value)
        => string.IsNullOrEmpty(value);
    
    public static string? ToLowerUi(this string? value)
        => value?.ToLower(CultureInfo.CurrentCulture);
}