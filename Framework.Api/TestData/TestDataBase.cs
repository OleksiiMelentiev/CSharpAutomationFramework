namespace Framework.Api.TestData;

public class TestDataBase
{
    public static string GetRandomString(int length = 8, string charsToChoose = "", string? charsToSkip = null)
    {
        var random = new Random();

        var chars = string.IsNullOrEmpty(charsToChoose)
            ? "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            : charsToChoose;

        if (string.IsNullOrEmpty(charsToSkip) == false)
        {
            foreach (var charToSkip in charsToSkip)
            {
                chars = chars.Replace(charToSkip.ToString(), "");
            }
        }

        return new string(
            Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
    }
}