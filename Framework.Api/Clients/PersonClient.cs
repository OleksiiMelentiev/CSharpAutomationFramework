using Common.Helpers;
using Models.Requests.Person;

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

    public async Task<HttpResponseMessage> GetAsync(Guid id)
    {
        var url = $"{_url}/{id}";
        return await HttpHelper.SendRequestAsync(HttpMethod.Get, url);
    }
}