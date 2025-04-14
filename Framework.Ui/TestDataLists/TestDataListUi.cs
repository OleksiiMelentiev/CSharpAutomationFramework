using Framework.Ui.TestData;

namespace Framework.Ui.TestDataLists;

public class TestDataListUi
{
    private TextBoxTestData? _textBox;
    public TextBoxTestData TextBox => _textBox ??= new TextBoxTestData();


    private static TestDataListUi? _instance;
    public static TestDataListUi Get() => _instance ??= new TestDataListUi();

    private TestDataListUi()
    {
    }
}