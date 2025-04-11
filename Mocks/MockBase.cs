using WireMock.Server;
using WireMock.Settings;
using WireMock.Types;

namespace Mocks;

public abstract class MockBase
{
    protected WireMockServer? Server { get; set; }


    public void RunServer()
    {
        if (Server != null)
        {
            return;
        }

        var settings = new WireMockServerSettings
        {
            Port = 5400,
            CorsPolicyOptions = CorsPolicyOptions.AllowAll
        };

        Server = WireMockServer.Start(settings);
    }

    public void StopServer()
    {
        Server?.Dispose();
    }
}