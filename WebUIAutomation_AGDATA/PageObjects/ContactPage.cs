using AGDATA_WebUIAutomation.WarpperFactory;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDATA_WebUIAutomation.PageObjects
{
    public class ContactPage : BasePage
    {
        private readonly IWebDriver _driver = BrowserFactory.Driver;
        By contactHeader = By.XPath("//section[@class='intro_header center-text']//h4[text()='Contact']"); // Adjust the XPath according to the actual header text on the Contact

        public bool IsLoaded()
        {
            // Return true if the header is displayed
            return _driver.FindElement(contactHeader).Displayed;
        }
    }
}
