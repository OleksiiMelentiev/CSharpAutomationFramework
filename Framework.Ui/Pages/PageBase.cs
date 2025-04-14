using Common.EnvConfigs;
using Framework.Ui.Helpers;
using Microsoft.Playwright;

namespace Framework.Ui.Pages;

public abstract class PageBase
{
    protected abstract string Url { get; }

    protected EnvConfig EnvConfig { get; } = EnvConfig.Get();

    protected PlaywrightHelper PlaywrightHelper => PlaywrightFactory.Get();
    public IPage Page => PlaywrightHelper.GetPage();


    public async Task OpenAsync(string? url = null)
    {
        if (url != null)
        {
            await Page.GotoAsync(url);
            return;
        }

        await Page.GotoAsync(Url);
    }
}