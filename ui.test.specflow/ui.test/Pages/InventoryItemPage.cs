using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class InventoryItemPage : Browser
    {
        public void addCartClick(string produto) => driver.FindElement(By.XPath("//*[@id=\"add-to-cart-" + produto.Replace(" ", "-").ToLower() + "\"]")).Click();
    }
}
