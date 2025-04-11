using System.Text;
using Common.Helpers;
using Models;
using Models.Requests;
using WireMock;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Types;
using WireMock.Util;

namespace Mocks;

public class PersonMock : MockBase
{
    private static readonly IList<Person> PersonDbSimulation = new List<Person>();

    public void SetMocks()
    {
        MockCreatePerson();
    }


    private void MockCreatePerson()
    {
        Server?.Given(
                Request.Create()
                    .WithPath("/api/person")
                    .UsingPost()
            )
            .RespondWith(Response.Create().WithCallback(request =>
            {
                var requestJson = request.BodyAsJson?.ToString() ?? string.Empty;
                var requestBody = JsonHelper.Deserialize<CreatePersonRequest>(requestJson);
                var person = new Person()
                {
                    Id = Guid.NewGuid(),
                    FName = requestBody.FName,
                    LName = requestBody.LName,
                    BirthDate = requestBody.BirthDate,
                };
                PersonDbSimulation.Add(person);

                return new ResponseMessage()
                {
                    StatusCode = 200,
                    BodyData = new BodyData
                    {
                        Encoding = Encoding.UTF8,
                        DetectedBodyType = BodyType.String,
                        BodyAsString = JsonHelper.Serialize(person),
                    },
                };
            }));
    }
}