using Framework.Ui.PagesLists;

namespace Framework.Ui.ExpectedResults;

public class ExpectedResultBase
{
    protected PagesList Pages { get; set; } = PagesList.Get();
}