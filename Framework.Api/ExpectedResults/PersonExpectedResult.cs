using System.Net;
using Common.Helpers;
using Models;
using Models.Requests.Person;
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

    public async Task<Person> CheckGetPersonResponseAsync(HttpResponseMessage response, Person expected)
    {
        await CheckStatusCodeAsync(response);

        var actual = await HttpHelper.GetModelFromResponseAsync<Person>(response);
        CheckPersonResponseBody(actual, expected);

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

    private void CheckPersonResponseBody(Person actual, Person expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.FName, Is.EqualTo(expected.FName));
            Assert.That(actual.LName, Is.EqualTo(expected.LName));
            Assert.That(actual.BirthDate, Is.EqualTo(expected.BirthDate));
        });
    }


    public async Task CheckGetNonExistingPersonResponseAsync(HttpResponseMessage response)
    {
        await CheckStatusCodeAsync(response, HttpStatusCode.NotFound);

        var responseStr = await HttpHelper.GetStringFromResponseAsync(response);
        Assert.That(responseStr, Is.EqualTo(string.Empty));
    }
}