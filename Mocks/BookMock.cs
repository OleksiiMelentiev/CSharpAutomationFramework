using System.Text;
using Common.Helpers;
using Models;
using Models.Requests.Book;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Types;
using WireMock.Util;

namespace Mocks;

public class BookMock : MockBase
{
    private static readonly IList<Book> BookDbSimulation = new List<Book>();

    public void SetMocks()
    {
        MockCreateBook();
        MockGetBook();
    }


    private void MockGetBook()
    {
        Server?.Given(
                Request.Create()
                    .WithPath("/api/book/*")
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithCallback(request =>
            {
                var pathSegments = request.Path.Split('/');
                var id = Guid.Parse(pathSegments.Last());

                var book = BookDbSimulation.FirstOrDefault(b => b.Id == id);

                var statusCode = book == null ? 404 : 200;
                var responseJson = book == null ? string.Empty : JsonHelper.Serialize(book);

                return new ResponseMessage()
                {
                    StatusCode = statusCode,
                    BodyData = new BodyData
                    {
                        Encoding = Encoding.UTF8,
                        DetectedBodyType = BodyType.String,
                        BodyAsString = responseJson,
                    },
                };
            }));
    }

    private void MockCreateBook()
    {
        Server?.Given(
                Request.Create()
                    .WithPath("/api/book")
                    .UsingPost()
            )
            .RespondWith(Response.Create().WithCallback(request =>
            {
                var requestJson = request.BodyAsJson?.ToString() ?? string.Empty;
                var requestBody = JsonHelper.Deserialize<CreateBookRequest>(requestJson);
                var book = new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = requestBody.Title,
                    Author = requestBody.Author,
                    PublicationDate = requestBody.PublicationDate,
                };
                BookDbSimulation.Add(book);

                return new ResponseMessage()
                {
                    StatusCode = 200,
                    BodyData = new BodyData
                    {
                        Encoding = Encoding.UTF8,
                        DetectedBodyType = BodyType.String,
                        BodyAsString = JsonHelper.Serialize(book),
                    },
                };
            }));
    }
}
