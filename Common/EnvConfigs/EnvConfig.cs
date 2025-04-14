using Common.Enums;
using Common.Helpers;

namespace Common.EnvConfigs;

public class EnvConfig
{
    public string ApiUrl { get; }
    public string UiUrl { get; }
    public Env Env { get; }
    public string ReportDir { get; private set; }


    private static EnvConfig? _instance;

    public static EnvConfig Get()
    {
        return _instance ??= new EnvConfig();
    }

    private EnvConfig()
    {
        Env = ConfigReader.GetEnvironment();
        ReportDir = GetReportDir();
        ApiUrl = GetApiUrl();
        UiUrl = GetUiUrl();
    }




    private string GetApiUrl()
    {
        return Env switch
        {
            Env.Localhost => "http://localhost:5400/api",

            _ => throw new ArgumentOutOfRangeException(nameof(Env), "Not implemented")
        };
    }

    private static string GetReportDir()
    {
        var dir = ConfigReader.GetConfig("reportDirectory");

        if (string.IsNullOrEmpty(dir))
        {
            var projDir = IoHelper.GetSolutionDirectory();
            dir = Path.Combine(projDir, "TestResult");
        }

        if (dir.Contains("PROJECT_NAME"))
        {
            var projDir = IoHelper.GetProjectDirectory(false);
            dir = dir.Replace("PROJECT_NAME", projDir);
        }

        if (dir.Contains("ENV_NAME"))
        {
            var env = ConfigReader.GetEnvironment();
            dir = dir.Replace("ENV_NAME", env.ToString().ToLower());
        }

        IoHelper.CreateFolderIfDoesNotExist(dir);

        return dir;
    }
    
    private string? GetUiUrl()
    {
        return Env switch
        {
            Env.Localhost => "https://demoqa.com",

            _ => throw new ArgumentOutOfRangeException(nameof(Env), "Not implemented")
        };
    }
}