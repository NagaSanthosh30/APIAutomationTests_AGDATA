using WebUIAutomation_AGDATA.PageBase;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace WebUITests_AGDATA.StepDefinitions
{
    [Binding]
    public class ContactPageFlowSteps
    { 

            [Given(@"User Navigating to MarketIntelligence")]
            public void GivenUserNavigatingToMarketIntelligence()
            {
                Page.HomePage.NavigateToMarketIntelligence();
            }


            [When(@"Follwing headings should be present '([^']*)', '([^']*)', '([^']*)'")]
            public void WhenFollwingHeadingsShouldBePresent(string heading1, string heading2, string heading3)
            {

                var headings = Page.MarketIntelligence.GetHeadings();
                headings.Contains(heading1).Should().BeTrue("Minimize Costs heading not found");
                headings.Contains(heading2).Should().BeTrue("Generate Revenue heading not found");
                headings.Contains(heading3).Should().BeTrue("Mitigate Risk heading not found");
            }

            [When(@"click on LetsGetStarted")]
            public void WhenClickOnLetsGetStarted()
            {

                Page.MarketIntelligence.ClickLetsGetStarted();
            }


            [Then(@"Contact Page should be Displayed")]
            public void ThenContactPageIsDisplayed()
            {
                Page.ContactPage.IsLoaded();

            }

        }

    }

