using Framework.Api.Tests;
using Tests.Api.TestDataSources;

namespace Tests.Api.Tests;

public class PersonTests : ApiTestBase
{
    [Test]
    [TestCaseSource(typeof(PersonTds), nameof(PersonTds.Create))]
    public async Task CreatePerson(bool includeBirthDate)
    {
        var request = TestData.Person.GetCreatePersonRequest(includeBirthDate);

        var response = await Clients.Person.CreateAsync(request);
        
        await ExpectedResults.Person.CheckCreatePersonResponseAsync(response, request);
    }
}