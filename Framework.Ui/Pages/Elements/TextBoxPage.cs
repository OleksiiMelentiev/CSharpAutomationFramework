using Microsoft.Playwright;
using Models.Ui.Elements;

namespace Framework.Ui.Pages.Elements;

public class TextBoxPage : PageBase
{
    protected override string Url => EnvConfig.UiUrl + "/text-box";

    private ILocator FullNameInput => Page.Locator("//input[@id='userName']");
    private ILocator EmailInput => Page.Locator("//input[@id='userEmail']");
    private ILocator CurrentAddressInput => Page.Locator("//textarea[@id='currentAddress']");
    private ILocator PermanentAddressInput => Page.Locator("//textarea[@id='permanentAddress']");
    private ILocator SubmitBtn => Page.Locator("#submit");


    public async Task<TextBox> GetSubmitResultAsync()
    {
        return new TextBox()
        {
            FullName = await GetResultAsync("//*[@id='output']//*[@id='name']"),
            Email = await GetResultAsync("//*[@id='output']//*[@id='email']"),
            CurrentAddress = await GetResultAsync("//*[@id='output']//*[@id='currentAddress']"),
            PermanentAddress = await GetResultAsync("//*[@id='output']//*[@id='permanentAddress']"),
        };
    }


    private async Task<string?> GetResultAsync(string locator)
    {
        var text = await Page.Locator(locator).TextContentAsync();

        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        return text.Split(':').Last().Trim();
    }

    public async Task SubmitFormAsync(TextBox textBox)
    {
        await FillInFullNameAsync(textBox.FullName);
        await FillInEmailAsync(textBox.Email);
        await FillInCurrentAddressAsync(textBox.CurrentAddress);
        await FillInPermanentAddressAsync(textBox.PermanentAddress);
        await ClickSubmitButtonAsync();
    }


    private async Task FillInFullNameAsync(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        await FullNameInput.FillAsync(name);
    }

    private async Task FillInEmailAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return;
        }

        await EmailInput.FillAsync(email);
    }

    private async Task FillInCurrentAddressAsync(string? currentAddress)
    {
        if (string.IsNullOrWhiteSpace(currentAddress))
        {
            return;
        }

        await CurrentAddressInput.FillAsync(currentAddress);
    }

    private async Task FillInPermanentAddressAsync(string? permanentAddress)
    {
        if (string.IsNullOrWhiteSpace(permanentAddress))
        {
            return;
        }

        await PermanentAddressInput.FillAsync(permanentAddress);
    }

    private async Task ClickSubmitButtonAsync()
    {
        await SubmitBtn.ClickAsync();
    }
}