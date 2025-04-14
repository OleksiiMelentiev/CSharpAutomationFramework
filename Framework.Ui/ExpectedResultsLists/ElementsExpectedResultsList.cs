using Framework.Ui.ExpectedResults.Elements;

namespace Framework.Ui.ExpectedResultsLists;

public class ElementsExpectedResultsList
{
    private TextBoxExpectedResult? _textBox;
    public TextBoxExpectedResult TextBox => _textBox ??= new TextBoxExpectedResult();
}