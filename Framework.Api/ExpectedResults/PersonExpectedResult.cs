using Framework.Api.Helpers;
using Framework.Api.Models;
using Framework.Api.Models.Requests;
using NUnit.Framework;

namespace Framework.Api.ExpectedResults;

public class PersonExpectedResult : ExpectedResultBase
{
    public async Task<Person> CheckCreatePersonResponseAsync(HttpResponseMessage response, CreatePersonRequest request)
    {
        await CheckStatusCodeAsync(response);

        var actual = await HttpHelper.GetModelFromResponseAsync<Person>(response);
        CheckPersonResponseBody(actual, request);

        return actual;
    }

    private void CheckPersonResponseBody(Person actual, CreatePersonRequest request)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(actual.FName, Is.Not.EqualTo(request.FName));
            Assert.That(actual.LName, Is.Not.EqualTo(request.LName));
            Assert.That(actual.BirthDate, Is.Not.EqualTo(request.BirthDate));
        });
    }
}