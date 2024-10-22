using WebUIAutomation_AGDATA.WapperFactory;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUIAutomation_AGDATA.PageObjects
{
    public class HomePage : BasePage
    {

        private readonly IWebDriver _driver = BrowserFactory.Driver;

        By solutionsMenu = By.XPath("//a[contains(text(),'Solutions')]");
        By marketIntelligenceMenu = By.XPath("(//a[(text()='Market Intelligence')])[1]");

        public void NavigateToMarketIntelligence()
        {
            ClickOnElement(solutionsMenu);
            ClickOnElement(marketIntelligenceMenu);

        }
        

    }
}
