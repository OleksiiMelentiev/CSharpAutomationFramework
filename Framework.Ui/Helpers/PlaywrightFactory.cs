using NUnit.Framework;

namespace Framework.Ui.Helpers;

public static class PlaywrightFactory
{
    private static readonly object Padlock = new();
    private static Dictionary<string, PlaywrightHelper> _playwrights = new();

    public static PlaywrightHelper Get()
    {
        lock (Padlock)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            _playwrights.TryGetValue(testName, out var playwright);

            if (playwright == null)
            {
                playwright = new PlaywrightHelper();
                _playwrights.Add(testName, playwright);
            }

            return playwright;
        }
    }
}