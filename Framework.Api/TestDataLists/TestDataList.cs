using Framework.Api.TestData;

namespace Framework.Api.TestDataLists;

public class TestDataList
{
    private PersonTestData? _person;
    public PersonTestData Person => _person ??= new PersonTestData();


    private static TestDataList? _instance;
    public static TestDataList Get() => _instance ??= new TestDataList();

    private TestDataList()
    {
    }
}