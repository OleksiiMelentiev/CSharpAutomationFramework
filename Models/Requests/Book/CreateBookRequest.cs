namespace Models.Requests.Book;

public class CreateBookRequest
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public DateTime? PublicationDate { get; set; }
}
