namespace Models.Requests.Person;

public class CreatePersonRequest
{
    public required string FName { get; set; }
    public required string LName { get; set; }
    public DateTime? BirthDate { get; set; }
}