using TechTalk.SpecFlow;
using ui.test.Drivers;

namespace ui.test.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [AfterScenario]
        public void AfterScenario()
        {
            Browser.closeDriver();
        }
    }
}
