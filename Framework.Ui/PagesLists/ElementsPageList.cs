using Framework.Ui.Pages.Elements;

namespace Framework.Ui.PagesLists;

public class ElementsPageList
{
    private TextBoxPage? _textBox;
    public TextBoxPage TextBox => _textBox ??= new TextBoxPage();
}