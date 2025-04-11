using Common.Helpers;
using Models;
using Models.Requests;
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
            Assert.That(actual.FName, Is.EqualTo(request.FName));
            Assert.That(actual.LName, Is.EqualTo(request.LName));
            Assert.That(actual.BirthDate, Is.EqualTo(request.BirthDate));
        });
    }
}