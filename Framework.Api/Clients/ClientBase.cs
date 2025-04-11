using Framework.Api.EnvConfigs;

namespace Framework.Api.Clients;

public class ClientBase
{
    protected EnvConfig EnvConfig = EnvConfig.Get();
}