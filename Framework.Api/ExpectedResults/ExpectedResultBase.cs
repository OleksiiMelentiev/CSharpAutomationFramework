using System.Net;
using Framework.Api.Helpers;
using NUnit.Framework;

namespace Framework.Api.ExpectedResults;

public class ExpectedResultBase
{
    public async Task CheckStatusCodeAsync(HttpResponseMessage response,
        HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    {
        try
        {
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode));
        }
        catch
        {
            MyLogger.LogInfo($"Expected status code {expectedStatusCode}, But was {response.StatusCode}");
            MyLogger.LogInfo(await HttpHelper.GetStringFromResponseAsync(response));
            throw;
        }
    }
}