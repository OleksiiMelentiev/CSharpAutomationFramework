using NUnit.Framework;

namespace Tests.Api.TestDataSources;

public static class BookTds
{
    public static IEnumerable<TestCaseData> Create()
    {
        yield return new TestCaseData(true);
        yield return new TestCaseData(false);
    }
}
