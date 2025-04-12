using Common.Enums;
using Common.EnvConfigs;
using Framework.Api.MocksLists;

namespace Tests.Api;

[SetUpFixture]
public class ApiTestSetUp
{
    private EnvConfig EnvConfig { get; } = EnvConfig.Get();
    private MocksList Mocks { get; } = MocksList.Get();

    [OneTimeSetUp]
    public void HppApiMocksOneTimeSetUp()
    {
        if (EnvConfig.Env == Env.Localhost)
        {
            Mocks.Run();
        }
        else
        {
            throw new ArgumentException($"Not implemented for the {EnvConfig.Env} env");
        }
    }


    [OneTimeTearDown]
    public void PopApiTestBaseOneTimeTearDown()
    {
        Mocks.Dispose();
    }
}