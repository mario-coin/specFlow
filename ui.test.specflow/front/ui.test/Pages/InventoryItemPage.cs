using OpenQA.Selenium;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class InventoryItemPage : Browser
    {
        private IWebElement productPriceTxt => driver.FindElement(By.XPath("//*[@id=\"inventory_item_container\"]/div/div/div[2]/div[3]"));
        private IWebElement backToInventoryBtn => driver.FindElement(By.XPath("//*[@id=\"back-to-products\"]"));
        public string getProductPrice() => productPriceTxt.Text;
        public void addCartClick(string produto) => driver.FindElement(By.XPath("//*[@id=\"add-to-cart-" + produto.Replace(" ", "-").ToLower() + "\"]")).Click();
        public void backToInventoryClick() => backToInventoryBtn.Click();
    }
}
