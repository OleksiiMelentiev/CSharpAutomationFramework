using Framework.Api.ClientsLists;
using Framework.Api.ExpectedResultsLists;
using Framework.Api.TestDataLists;

namespace Framework.Api.Tests;

public class ApiTestBase : TestBase
{
    protected readonly ClientsList Clients = ClientsList.Get();
    protected readonly ExpectedResultsList ExpectedResults = ExpectedResultsList.Get();
    protected readonly TestDataList TestData = TestDataList.Get();
}