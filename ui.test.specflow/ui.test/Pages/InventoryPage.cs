using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class InventoryPage : Browser
    {
        private IWebElement menuBtn => driver.FindElement(By.XPath("//*[@id=\"react-burger-menu-btn\"]"));
        private IWebElement menuWindow => driver.FindElement(By.XPath("//*[@id=\"menu_button_container\"]/div/div[2]"));
        private IWebElement logoutLink => driver.FindElement(By.XPath("//*[@id=\"logout_sidebar_link\"]"));

        public void menuClick() => menuBtn.Click();
        public bool isMenuWindowVisible() => menuWindow.Displayed;
        public bool isLogoutExist() => logoutLink.Displayed;
        public void getProduto(string produto) => driver.FindElement(By.XPath(""));
    }
}
