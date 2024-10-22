using WebUIAutomation_AGDATA.WapperFactory;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebUIAutomation_AGDATA.PageObjects
{
    public class BasePage
    {
        private readonly IWebDriver _driver = BrowserFactory.Driver;

        public void EnterText(By element,string text,string textBoxName="")
        {
            try
            {
                _driver.FindElement(element).SendKeys(text);

            }
            catch (Exception)
            {

            }
        }

        public void ClickOnElement(By element, string elementName = "")
        {
            try
            {
                _driver.FindElement(element).Click();

            }
            catch (Exception)
            {

            }
        }

       
        public void ScrollToElement(IWebElement element)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scroll(" + element.Location.X + "," + (element.Location.Y - 500) + ");");

        }
        public void ScrollToBottom()
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        public void JavaScriptClick(IWebElement element)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        public void UploadFile(IWebElement element, string filename)
        {
            string File = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\OPSLLAutomation\\TestData\\" + filename;
            element.SendKeys(File);
        }
        public void SelectByText(IWebElement element, string text, bool partialMatch = false)
        {
            SelectElement oselect = new SelectElement(element);
            oselect.SelectByText(text, partialMatch);
        }

    }
}
