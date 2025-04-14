using Common.EnvConfigs;
using Common.Helpers;
using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework.Ui.Helpers;

public class PlaywrightHelper
{
    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private IPlaywright PlaywrightObj { get; set; } = default!;
    private IBrowser BrowserObj { get; set; } = default!;
    private IBrowserContext Context { get; set; } = default!;
    private IPage? Page { get; set; }
    private readonly bool _isDebugging = bool.Parse(ConfigReader.GetConfig("isDebugging"));

    private EnvConfig _envConfig = EnvConfig.Get();


    public async Task CloseAsync()
    {
        await _semaphore.WaitAsync();

        if (Page == null)
        {
            throw new NullReferenceException("The page is not created");
        }

        await Page.CloseAsync();
        await Context.DisposeAsync();
        await BrowserObj.DisposeAsync();
        PlaywrightObj.Dispose();

        _semaphore.Release();
    }


    public async Task CreateAsync()
    {
        await _semaphore.WaitAsync();

        PlaywrightObj = await Playwright.CreateAsync();
        BrowserObj = await CreateBrowserAsync();
        Page = await CreatePageAsync();

        _semaphore.Release();
    }

    public IPage GetPage()
    {
        return Page ?? throw new ArgumentException("Playwright instance is not created");
    }

    public async Task StartTracingAsync()
    {
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public async Task<string> StopTracing()
    {
        var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
        var tracePath = Path.Combine(
            _envConfig.ReportDir,
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
        );

        await Context.Tracing.StopAsync(new()
        {
            Path = isPassed ? null : tracePath
        });

        return tracePath;
    }

    public async Task<string> TakeScreenshotAsync()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
        var screenshotName = $"{TestContext.CurrentContext.Test.Name}_{timestamp}.png";
        var screenshotsDir = Path.Combine(_envConfig.ReportDir, "Screenshot");

        IoHelper.CreateFolderIfDoesNotExist(screenshotsDir);

        var screenshotPath = Path.Combine(screenshotsDir, screenshotName);

        var page = GetPage();
        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = screenshotPath,
            FullPage = true,
        });

        return screenshotPath;
    }


    private async Task<IBrowser> CreateBrowserAsync()
    {
        var browserType = ConfigReader.GetConfig("browserType");

        var options = _isDebugging
            ? new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--start-maximized" },
            }
            : new BrowserTypeLaunchOptions
            {
                // add LaunchOptions if needed
            };

        return browserType switch
        {
            BrowserType.Chromium => await PlaywrightObj.Chromium.LaunchAsync(options),
            BrowserType.Firefox => await PlaywrightObj.Chromium.LaunchAsync(options),

            _ => throw new ArgumentOutOfRangeException(nameof(browserType), "Unsupported browser")
        };
    }

    private async Task<IPage> CreatePageAsync()
    {
        Context = await BrowserObj.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });
        var page = await Context.NewPageAsync();

        if (_isDebugging == false)
        {
            await page.SetViewportSizeAsync(1920, 1080); // change page size if needed 
        }

        return page;
    }
}