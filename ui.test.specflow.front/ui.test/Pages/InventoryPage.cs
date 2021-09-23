using OpenQA.Selenium;
using System.Collections.Generic;
using ui.test.Drivers;
using ui.test.Model;

namespace ui.test.Pages
{
    class InventoryPage : Browser
    {
        private IWebElement menuBtn => driver.FindElement(By.XPath("//*[@id=\"react-burger-menu-btn\"]"));
        private IWebElement menuWindow => driver.FindElement(By.XPath("//*[@id=\"menu_button_container\"]/div/div[2]"));
        private IWebElement logoutLink => driver.FindElement(By.XPath("//*[@id=\"logout_sidebar_link\"]"));
        private IReadOnlyCollection<IWebElement> inventoryItemCards => driver.FindElements(By.CssSelector("#inventory_container div.inventory_list div.inventory_item"));
        public void menuClick() => menuBtn.Click();
        public bool isMenuWindowVisible() => menuWindow.Displayed;
        public bool isLogoutExist() => logoutLink.Displayed;
        public void getProduto(string produto) => driver.FindElement(By.XPath("//*[text()='" + produto + "']")).Click();
        public IEnumerable<Product> getInventoryItemCardPrice()
        {
            List<Product> products = new List<Product>();
            foreach (var inventoryItemCard in inventoryItemCards)
            {
                products.Add
                (
                    new Product
                    (
                        name: inventoryItemCard.FindElement(By.CssSelector(".inventory_item_name")).Text,
                        price: inventoryItemCard.FindElement(By.XPath(".//div[2]/div[2]/div")).Text
                    )
                );
            }
            return products;
        }
    }
}
