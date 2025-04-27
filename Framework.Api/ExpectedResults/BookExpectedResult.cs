using System.Net;
using Common.Helpers;
using Models;
using Models.Requests.Book;
using NUnit.Framework;

namespace Framework.Api.ExpectedResults;

public class BookExpectedResult : ExpectedResultBase
{
    public async Task<Book> CheckCreateBookResponseAsync(HttpResponseMessage response, CreateBookRequest request)
    {
        await CheckStatusCodeAsync(response);

        var actual = await HttpHelper.GetModelFromResponseAsync<Book>(response);
        CheckBookResponseBody(actual, request);

        return actual;
    }

    public async Task<Book> CheckGetBookResponseAsync(HttpResponseMessage response, Book expected)
    {
        await CheckStatusCodeAsync(response);

        var actual = await HttpHelper.GetModelFromResponseAsync<Book>(response);
        CheckBookResponseBody(actual, expected);

        return actual;
    }

    private void CheckBookResponseBody(Book actual, CreateBookRequest request)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(actual.Title, Is.EqualTo(request.Title));
            Assert.That(actual.Author, Is.EqualTo(request.Author));
            Assert.That(actual.PublicationDate, Is.EqualTo(request.PublicationDate));
        });
    }

    private void CheckBookResponseBody(Book actual, Book expected)
    {
        Assert.Multiple(() =>
        {
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Author, Is.EqualTo(expected.Author));
            Assert.That(actual.PublicationDate, Is.EqualTo(expected.PublicationDate));
        });
    }

    public async Task CheckGetNonExistingBookResponseAsync(HttpResponseMessage response)
    {
        await CheckStatusCodeAsync(response, HttpStatusCode.NotFound);

        var responseStr = await HttpHelper.GetStringFromResponseAsync(response);
        Assert.That(responseStr, Is.EqualTo(string.Empty));
    }
}
