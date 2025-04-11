using Framework.Api.Enums;
using Framework.Api.Extensions;
using Microsoft.Extensions.Configuration;

namespace Framework.Api.Helpers;

public static class ConfigReader
{
    private static readonly IConfiguration Config;
    private static readonly IConfiguration ConfigLocal;


    static ConfigReader()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        ConfigLocal = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .Build();
    }


    public static string GetConfig(string configName)
    {
        var envValue = Environment.GetEnvironmentVariable(configName);
        if (string.IsNullOrEmpty(envValue) == false)
        {
            return envValue;
        }

        var localValue = ConfigLocal[configName];
        if (string.IsNullOrEmpty(localValue) == false)
        {
            return localValue;
        }

        return Config[configName] ?? throw new ArgumentException($"Can't read '{configName}' config");
    }
    
    public static Env GetEnvironment()
    {
        var envStr = GetConfig("env");
        return envStr.ToEnum<Env>();
    }
}