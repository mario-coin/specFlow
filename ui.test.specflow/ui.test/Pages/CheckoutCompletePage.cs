using OpenQA.Selenium;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class CheckoutCompletePage : Browser
    {
        private IWebElement thankYouForYourOrderTxt => driver.FindElement(By.XPath("//*[@id=\"checkout_complete_container\"]/h2"));

        public string getThankYouForYourOrder() => thankYouForYourOrderTxt.Text;
    }
}
