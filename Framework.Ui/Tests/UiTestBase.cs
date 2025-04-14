using Framework.Api.Tests;
using Framework.Ui.ExpectedResultsLists;
using Framework.Ui.Helpers;
using Framework.Ui.PagesLists;
using Framework.Ui.TestDataLists;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework.Ui.Tests;

public class UiTestBase : TestBase
{
    protected ExpectedResultsListUi ExpectedResults { get; set; } = ExpectedResultsListUi.Get();
    protected PagesList Pages { get; private set; } = PagesList.Get();
    protected readonly TestDataListUi TestData = TestDataListUi.Get();

    private PlaywrightHelper Playwright => PlaywrightFactory.Get();


    [SetUp]
    public async Task Setup()
    {
        await Playwright.CreateAsync();
        await Playwright.StartTracingAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        try
        {
            var tracingPath = await Playwright.StopTracing();

            var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            if (isPassed == false)
            {
                var screenPath = await Playwright.TakeScreenshotAsync();
                ExtentReports.LogScreenshot(screenPath);
                ExtentReports.LogScreenshot(tracingPath, "tracing (open in a new tab to download)");
            }
        }
        finally
        {
            await Playwright.CloseAsync();
        }
    }
}