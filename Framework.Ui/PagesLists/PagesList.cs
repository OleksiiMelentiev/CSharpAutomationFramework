namespace Framework.Ui.PagesLists;

public class PagesList
{
    private ElementsPageList? _elements;
    public ElementsPageList Elements => _elements ??= new ElementsPageList();

    private static PagesList? _instance;
    public static PagesList Get() => _instance ??= new PagesList();

    private PagesList()
    {
    }
}