using Framework.Ui.ExpectedResults;

namespace Framework.Ui.ExpectedResultsLists;

public class ExpectedResultsListUi
{
    private ElementsExpectedResultsList? _elements;
    public ElementsExpectedResultsList Elements => _elements ??= new ElementsExpectedResultsList();

    private static ExpectedResultsListUi? _instance;
    public static ExpectedResultsListUi Get() => _instance ??= new ExpectedResultsListUi();

    private ExpectedResultsListUi()
    {
    }
}