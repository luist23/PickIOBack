namespace BaseProject.Models.Extensions;

public static class StringExtension
{
    public static string NormalizeText(this string str)
    {
        return str?.Trim().ToUpperInvariant() ?? string.Empty;
    }
}