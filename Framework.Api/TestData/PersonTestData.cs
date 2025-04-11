using Models.Requests;

namespace Framework.Api.TestData;

public class PersonTestData : TestDataBase
{
    public CreatePersonRequest GetCreatePersonRequest(bool includeBirthDate)
    {
        return new CreatePersonRequest()
        {
            FName = GetRandomString(),
            LName = GetRandomString(),
            BirthDate = includeBirthDate ? DateTime.UtcNow.AddYears(-18) : null,
        };
    }
}