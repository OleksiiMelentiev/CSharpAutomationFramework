using System.Collections.Concurrent;
using AventStack.ExtentReports;
using Common.EnvConfigs;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Common.Helpers;

public class ExtentManager
{
    private static readonly object Padlock = new();
    private static readonly ConcurrentDictionary<string, ExtentTest> ExtentMap = new();
    private static ExtentReports? _extentReports;
    private static EnvConfig EnvConfig { get; set; } = EnvConfig.Get();

    public static readonly string ReportDirectory = EnvConfig.ReportDir;

    private static ExtentManager? _instance;


    public void CreateTest(string testName, string? testCategory)
    {
        lock (Padlock)
        {
            var extentTest = GetReporting().CreateTest(testName);
            if (testCategory != null)
            {
                extentTest.AssignCategory(testCategory);
            }

            ExtentMap.TryAdd(testName, extentTest);
        }
    }

    public static ExtentManager Get()
    {
        lock (Padlock)
        {
            return _instance ??= new ExtentManager();
        }
    }

    public void EndReporting()
    {
        lock (Padlock)
        {
            GetReporting().Flush();
        }
    }

    public void EndTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == TestStatus.Passed)
        {
            GetExtentTest().Pass();
        }
        else
        {
            var exception = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? string.Empty
                : $"<pre>{TestContext.CurrentContext.Result.Message}</pre>";
            exception += $"<pre>{TestContext.CurrentContext.Result.StackTrace}</pre><br>";
            GetExtentTest().Fail(exception);
        }

        lock (Padlock)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            ExtentMap.TryRemove(testName, out _);
        }
    }

    public void LogInfo(string message)
    {
        lock (Padlock)
        {
            GetExtentTest().Log(Status.Info, message);
        }
    }

    public void LogScreenshot(string imagePath, string? title = null, string? info = null)
    {
        lock (Padlock)
        {
            GetExtentTest().Info(info).AddScreenCaptureFromPath(imagePath, title);
        }
    }


    private ExtentTest GetExtentTest()
    {
        lock (Padlock)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            return ExtentMap[testName];
        }
    }

    private ExtentReports GetReporting()
    {
        lock (Padlock)
        {
            if (_extentReports == null)
            {
                var reportFilePath = Path.Combine(ReportDirectory, "index.html");
                var reporter = new AventStack.ExtentReports.Reporter.ExtentSparkReporter(reportFilePath);
                _extentReports = new ExtentReports();
                _extentReports.AttachReporter(reporter);
            }

            return _extentReports;
        }
    }
}