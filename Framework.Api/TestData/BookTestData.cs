using Models.Requests.Book;

namespace Framework.Api.TestData;

public class BookTestData : TestDataBase
{
    public CreateBookRequest GetCreateBookRequest(bool includePublicationDate)
    {
        return new CreateBookRequest()
        {
            Title = GetRandomString(),
            Author = GetRandomString(),
            PublicationDate = includePublicationDate ? DateTime.UtcNow.AddYears(-1) : null,
        };
    }
}
