using Models.Ui.Elements;
using NUnit.Framework;

namespace Framework.Ui.ExpectedResults.Elements;

public class TextBoxExpectedResult : ExpectedResultBase
{
    public async Task CheckSubmitFormResultAsync(TextBox expected)
    {
        var actual = await Pages.Elements.TextBox.GetSubmitResultAsync();
        
        Assert.Multiple(() =>
        {
            Assert.That(actual.FullName, Is.EqualTo(expected.FullName));
            Assert.That(actual.Email, Is.EqualTo(expected.Email));
            Assert.That(actual.CurrentAddress, Is.EqualTo(expected.CurrentAddress));
            Assert.That(actual.PermanentAddress, Is.EqualTo(expected.PermanentAddress));
        });
    }
}