namespace Tests.Api.TestDataSources;

public static class PersonTds
{
    public static IEnumerable<TestCaseData> Create()
    {
        yield return new TestCaseData(true);
        yield return new TestCaseData(false);
    }
}