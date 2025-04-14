namespace Framework.Ui.TestDataLists;

public class TestDataListUi
{
    private ElementsTestDataList? _elements;
    public ElementsTestDataList Elements => _elements ??= new ElementsTestDataList();


    private static TestDataListUi? _instance;
    public static TestDataListUi Get() => _instance ??= new TestDataListUi();

    private TestDataListUi()
    {
    }
}