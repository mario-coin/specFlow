using OpenQA.Selenium;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class CheckoutStepTwoPage : Browser
    {
        private IWebElement finishBtn => driver.FindElement(By.XPath("//*[@id=\"finish\"]"));

        public void finishClick() => finishBtn.Click();
    }
}
