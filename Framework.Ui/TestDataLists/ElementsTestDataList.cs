using Framework.Ui.TestData.Elements;

namespace Framework.Ui.TestDataLists;

public class ElementsTestDataList
{
    private TextBoxTestData? _textBox;
    public TextBoxTestData TextBox => _textBox ??= new TextBoxTestData();
}