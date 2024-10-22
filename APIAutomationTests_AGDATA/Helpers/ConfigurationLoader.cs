using Newtonsoft.Json;

namespace APIAutomationTests_AGDATA.Helpers
{
    public class Configuration
    {
        public string BaseUrl { get; set; }
        public string AuthToken { get; set; }
    }

    public static class ConfigurationLoader
    {
        public static Configuration LoadConfig()
        {
            var configFilePath = Directory.GetCurrentDirectory()+"\\config.json";
            var json = File.ReadAllText(configFilePath);
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }

}
