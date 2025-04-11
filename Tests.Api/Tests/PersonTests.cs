using Common.Helpers;
using Framework.Api.Tests;
using Models;
using Tests.Api.TestDataSources;

namespace Tests.Api.Tests;

[TestFixture]
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

    [Test]
    public async Task GetPerson()
    {
        // preconditions. todo: create using TestData
        var createPersonRequest = TestData.Person.GetCreatePersonRequest(true);
        var createPersonRequestResponse = await Clients.Person.CreateAsync(createPersonRequest);
        var createdPerson = await HttpHelper.GetModelFromResponseAsync<Person>(createPersonRequestResponse);


        // test
        var response = await Clients.Person.GetAsync(createdPerson.Id);

        await ExpectedResults.Person.CheckGetPersonResponseAsync(response, createdPerson);
    }
    
    [Test]
    public async Task GetPerson_NoSuchPerson()
    {
        // test
        var response = await Clients.Person.GetAsync(Guid.NewGuid());

        await ExpectedResults.Person.CheckGetNonExistingPersonResponseAsync(response);
    }
}