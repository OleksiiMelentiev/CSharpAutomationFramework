using Common.Helpers;
using Framework.Api.Tests;
using Models;
using Tests.Api.TestDataSources;

namespace Tests.Api.Tests;

[TestFixture]
public class BookTests : ApiTestBase
{
    [Test]
    [TestCaseSource(typeof(BookTds), nameof(BookTds.Create))]
    public async Task CreateBook(bool includePublicationDate)
    {
        var request = TestData.Book.GetCreateBookRequest(includePublicationDate);

        var response = await Clients.Book.CreateAsync(request);

        await ExpectedResults.Book.CheckCreateBookResponseAsync(response, request);
    }

    [Test]
    public async Task GetBook()
    {
        // preconditions
        var createBookRequest = TestData.Book.GetCreateBookRequest(true);
        var createBookRequestResponse = await Clients.Book.CreateAsync(createBookRequest);
        var createdBook = await HttpHelper.GetModelFromResponseAsync<Book>(createBookRequestResponse);

        // test
        var response = await Clients.Book.GetAsync(createdBook.Id);

        await ExpectedResults.Book.CheckGetBookResponseAsync(response, createdBook);
    }
    
    [Test]
    public async Task GetBook_NoSuchBook()
    {
        // test
        var response = await Clients.Book.GetAsync(Guid.NewGuid());

        await ExpectedResults.Book.CheckGetNonExistingBookResponseAsync(response);
    }
}
