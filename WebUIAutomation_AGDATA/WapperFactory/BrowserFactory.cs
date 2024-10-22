using WebUIAutomation_AGDATA.Utilities;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace WebUIAutomation_AGDATA.WapperFactory
{
    public class BrowserFactory
    {
        #region Private fields
        [ThreadStatic]
        private static IWebDriver _driver;
        [ThreadStatic]
        private static EnvironmentDetails environmentDetails;
        #endregion
        #region Properties
        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    InitBrowser(environmentDetails.BrowserName);
                }
                return _driver;
            }
            private set
            {
                _driver = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// This Method Creates Instance of Browser will be used to rest of Execution of Test case
        /// </summary>
        /// <param name="Names of the Browser"></param>
        public static void InitBrowser(string browserName)
        {

            switch (browserName.ToLower())
            {
                case "Firefox":
                    _driver = new FirefoxDriver();
                    break;

                case "edge":
                    _driver = new EdgeDriver();
                    break;

                case "chrome":
                    _driver = new ChromeDriver();
                    break;
            }
            _driver.Manage().Window.Maximize();

        }

        /// <summary>
        /// This Method is Used to d the Application URL in Web Browser
        /// </summary>
        /// <param name="Application URL"></param>
        public static void LoadApplication(string url)
        {
            _driver.Url = url;
        }


        /// <summary>
        /// This Method is Used to Close the all Browser Instances 
        /// </summary>
        public static void CloseAllDrivers()
        {

            _driver.Quit();
        }

        public static MediaEntityModelProvider GetScreenshot()
        {
            DateTime time = DateTime.Now;
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, time.ToString()).Build();
        }
        #endregion 
    }
}
