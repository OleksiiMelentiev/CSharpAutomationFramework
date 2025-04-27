using Common.Helpers;
using Models.Requests.Book;

namespace Framework.Api.Clients;

public class BookClient : ClientBase
{
    private readonly string _url;
    
    public BookClient()
    {
        _url = $"{EnvConfig.ApiUrl}/book";
    }
    
    public async Task<HttpResponseMessage> CreateAsync(CreateBookRequest request)
    {
        return await HttpHelper.SendRequestAsync(HttpMethod.Post, _url, request);
    }
    
    public async Task<HttpResponseMessage> GetAsync(Guid id)
    {
        var url = $"{_url}/{id}";
        return await HttpHelper.SendRequestAsync(HttpMethod.Get, url);
    }
}
