using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using ui.test.Drivers;

namespace ui.test.Pages
{
    class LoginPage : Browser
    {
        private IWebElement botImg => driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[2]/div[1]/div[2]"));
        public bool isBotImgExist() => botImg.Displayed;
    }
}
