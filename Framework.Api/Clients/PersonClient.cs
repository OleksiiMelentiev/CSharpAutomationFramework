using Framework.Api.Helpers;
using Framework.Api.Models.Requests;

namespace Framework.Api.Clients;

public class PersonClient : ClientBase
{
    private readonly string _url;


    public PersonClient()
    {
        _url = $"{EnvConfig.ApiUrl}/person";
    }


    public async Task<HttpResponseMessage> CreateAsync(CreatePersonRequest request)
    {
        return await HttpHelper.SendRequestAsync(HttpMethod.Post, _url, request);
    }
}