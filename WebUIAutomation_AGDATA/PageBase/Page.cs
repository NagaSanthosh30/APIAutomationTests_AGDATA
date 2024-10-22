using WebUIAutomation_AGDATA.PageObjects;

namespace WebUIAutomation_AGDATA.PageBase
{
    public class Page
    {
        public static ContactPage ContactPage => new ContactPage();
        public static HomePage HomePage => new HomePage();
        public static MarketIntelligence MarketIntelligence => new MarketIntelligence();
        public static BasePage BasePage => new BasePage();

    }
}
