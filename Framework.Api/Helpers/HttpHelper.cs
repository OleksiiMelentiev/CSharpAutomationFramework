using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Framework.Api.Helpers;

public static class HttpHelper
{
    public static async Task<T> GetModelFromResponseAsync<T>(HttpResponseMessage response)
    {
        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonHelper.Deserialize<T>(responseJson)
               ?? throw new ArgumentException("Can't get correct answer from api");
    }
    
    public static async Task<string> GetStringFromResponseAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadAsStringAsync();
    }
    
    public static async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, object? body = null,
        string? token = null, IDictionary<string, string>? headers = null)
    {
        var request = new HttpRequestMessage(method, url);

        if (body != null)
        {
            var contentJson = JsonHelper.Serialize(body);
            request.Content = new StringContent(contentJson, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        using var client = new HttpClient();

        if (token != null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        return await client.SendAsync(request);
    }

}