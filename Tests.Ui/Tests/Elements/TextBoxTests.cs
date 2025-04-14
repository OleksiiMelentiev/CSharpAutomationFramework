using Framework.Ui.Tests;

namespace Tests.Ui.Tests.Elements;

[TestFixture]
public class TextBoxTests : UiTestBase
{
    [Test]
    public async Task PositiveExample()
    {
        var textBox = TestData.Elements.TextBox.GetRandom();

        await Pages.Elements.TextBox.OpenAsync();
        await Pages.Elements.TextBox.SubmitFormAsync(textBox);

        await ExpectedResults.Elements.TextBox.CheckSubmitFormResultAsync(textBox);
    }
    
    [Test]
    public async Task NegativeExample()
    {
        var textBox = TestData.Elements.TextBox.GetRandom();

        await Pages.Elements.TextBox.OpenAsync();
        await Pages.Elements.TextBox.SubmitFormAsync(textBox);

        textBox.FullName = "fail";

        await ExpectedResults.Elements.TextBox.CheckSubmitFormResultAsync(textBox);
    }
}