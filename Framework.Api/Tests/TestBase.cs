using Framework.Api.EnvConfigs;
using Framework.Api.Helpers;
using NUnit.Framework;

namespace Framework.Api.Tests;

[Parallelizable(ParallelScope.All)]
public class TestBase
{
    protected readonly EnvConfig EnvConfig = EnvConfig.Get();
    protected readonly ExtentManager ExtentReports = ExtentManager.Get();


    [SetUp]
    public void SetUpBase()
    {
        ExtentReports.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
    }

    [OneTimeTearDown]
    
    public void OneTimeTearDownBase()
    {
        ExtentReports.EndReporting();
    }

    [TearDown]
    public void TearDownBase()
    {
        ExtentReports.EndTest();
    }
}