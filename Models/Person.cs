using Models.Requests;

namespace Models;

public class Person : CreatePersonRequest
{
    public Guid Id { get; set; }
}