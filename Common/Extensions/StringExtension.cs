namespace Common.Extensions;

public static class StringExtension
{
    public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct => Enum.Parse<TEnum>(value, true);
}