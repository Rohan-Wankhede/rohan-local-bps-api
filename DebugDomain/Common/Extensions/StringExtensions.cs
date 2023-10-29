namespace DebugDomain.Common.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? text) => string.IsNullOrWhiteSpace(text);

    public static bool HasValidLength(this string input, int minLength, int maxLength, bool trimFirst = true)
    {
        var text = trimFirst ? input.Trim() : input;

        return text.Length >= minLength && text.Length <= maxLength;
    }

}