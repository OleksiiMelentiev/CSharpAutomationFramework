using Models.Requests.Book;

namespace Models;

public class Book : CreateBookRequest
{
    public Guid Id { get; set; }
}
