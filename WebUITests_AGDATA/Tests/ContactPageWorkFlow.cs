using WebUIAutomation_AGDATA.PageObjects;
using WebUIAutomation_AGDATA.Utilities;
using WebUIAutomation_AGDATA.WapperFactory;
using NUnit.Framework;
using System.Text.Json;

namespace WebUITests_AGDATA.Tests
{
    
    [TestFixture]
    public class EndToEndTests
    {
        private HomePage _homePage = null;
        private MarketIntelligence _marketIntelligencePage;
        private ContactPage _contactPage;
        private EnvironmentDetails _environmentDetails;

        [SetUp]
        public void SetUp()
        {
            // Load configuration from JSON
            var configJson = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Environment.json");
            _environmentDetails = JsonSerializer.Deserialize<EnvironmentDetails>(configJson);

            // Initialize the browser using the settings from the JSON file
            BrowserFactory.InitBrowser(_environmentDetails.BrowserName);


            // Load the application URL
            BrowserFactory.LoadApplication(_environmentDetails.AppURL);

            // Initialize page objects
            _homePage = new HomePage();
            _marketIntelligencePage = new MarketIntelligence();
            _contactPage = new ContactPage();
        }
        [Test]
        public void ContactPageWorkFlow()
        {
            // Navigate to Market Intelligence from Home Page
            _homePage.NavigateToMarketIntelligence();

            // Get headings from Market Intelligence page and verify
            var headings = _marketIntelligencePage.GetHeadings();
            // Assert the headings are present
            Assert.That(headings.Contains("MINIMIZE COSTS"), Is.True, "Minimize Costs heading not found");
            Assert.That(headings.Contains("GENERATE REVENUE"), Is.True, "Generate Revenue heading not found");
            Assert.That(headings.Contains("MITIGATE RISK"), Is.True, "Mitigate Risk heading not found");

            // Click on "Let's Get Started" button
            _marketIntelligencePage.ClickLetsGetStarted();

            // Verify that the Contact page is loaded
            Assert.That(_contactPage.IsLoaded(), "Contact page was not loaded correctly.");
        }

    }



}

