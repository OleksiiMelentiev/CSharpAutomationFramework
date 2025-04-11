using Framework.Api.ClientsLists;
using Framework.Api.ExpectedResultsLists;
using Framework.Api.TestDataLists;

namespace Framework.Api.Tests;

public class ApiTestBase
{
    protected ClientsList Clients = ClientsList.Get();
    protected ExpectedResultsList ExpectedResults = ExpectedResultsList.Get();
    protected TestDataList TestData = TestDataList.Get();
}