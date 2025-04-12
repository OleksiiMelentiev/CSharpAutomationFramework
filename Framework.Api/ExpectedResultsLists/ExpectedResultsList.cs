using Framework.Api.ExpectedResults;

namespace Framework.Api.ExpectedResultsLists;

public class ExpectedResultsList
{
    private PersonExpectedResult? _person;
    public PersonExpectedResult Person => _person ??= new PersonExpectedResult();


    private static ExpectedResultsList? _instance;
    public static ExpectedResultsList Get() => _instance ??= new ExpectedResultsList();

    private ExpectedResultsList()
    {
    }
}