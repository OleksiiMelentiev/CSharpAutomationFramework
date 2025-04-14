using Framework.Api.TestData;
using Models.Ui.Elements;

namespace Framework.Ui.TestData.Elements;

public class TextBoxTestData : TestDataBase
{
    public TextBox GetRandom(bool includeName = true)
    {
        return new TextBox()
        {
            FullName = includeName ? GetRandomString() : null,
            Email = $"{GetRandomString()}@{GetRandomString(2)}.{GetRandomString(3)}",
            CurrentAddress = $"{GetRandomString()} {GetRandomString()}",
            PermanentAddress = $"{GetRandomString()} {GetRandomString()}",
        };
    }
}