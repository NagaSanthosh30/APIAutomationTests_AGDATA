using AGDATA_WebUIAutomation.PageObjects;

namespace AGDATA_WebUIAutomation.PageBase
{
    public class Page
    {
        public static ContactPage ContactPage => new ContactPage();
        public static HomePage HomePage => new HomePage();
        public static MarketIntelligence MarketIntelligence => new MarketIntelligence();
        public static BasePage BasePage => new BasePage();

    }
}
