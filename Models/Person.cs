using Models.Requests.Person;

namespace Models;

public class Person : CreatePersonRequest
{
    public Guid Id { get; set; }
}