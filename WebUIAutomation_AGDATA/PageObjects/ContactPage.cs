using WebUIAutomation_AGDATA.WapperFactory;
using OpenQA.Selenium;

namespace WebUIAutomation_AGDATA.PageObjects
{
    public class ContactPage : BasePage
    {
        private readonly IWebDriver _driver = BrowserFactory.Driver;
        #region Locators
        By contactHeader = By.XPath("//section[@class='intro_header center-text']//h4[text()='Contact']"); // Adjust the XPath according to the actual header text on the Contact
        #endregion
#region Methods

        public bool IsLoaded()
        {
            // Return true if the header is displayed
            return _driver.FindElement(contactHeader).Displayed;
        }
        #endregion
    }
}
