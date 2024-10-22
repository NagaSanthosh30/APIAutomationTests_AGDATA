using WebUIAutomation_AGDATA.WapperFactory;
using OpenQA.Selenium;

namespace WebUIAutomation_AGDATA.PageObjects
{
    public class MarketIntelligence : BasePage
    {
        private readonly IWebDriver _driver = BrowserFactory.Driver;
        private By waysYouBenefitHeadings = By.XPath("//section[@class='services_target_markets three_col_textarea']/div/h3");
        private By letsGetStartedButton = By.XPath("//div[@id='prefooter']//a[text()=\"Let's Get Started\"]");

        public IList<string> GetHeadings()
        {
            
            var headings = _driver.FindElements(waysYouBenefitHeadings);
            return headings.Select(h => h.Text).ToList(); // Return headings as a list

        }

        // Click on "Let's Get Started" button
        public void ClickLetsGetStarted()
        {
            ClickOnElement(letsGetStartedButton);
        }
    }
}
