using Framework.Api.Models.Requests;

namespace Framework.Api.Models;

public class Person : CreatePersonRequest
{
    public Guid Id { get; set; }
}