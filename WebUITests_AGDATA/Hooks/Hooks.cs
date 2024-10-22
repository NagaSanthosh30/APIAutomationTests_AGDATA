using AGDATA_WebUIAutomation.Utilities;
using AGDATA_WebUIAutomation.WarpperFactory;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System.Reflection;
using TechTalk.SpecFlow;

namespace StackyonUITestsAGDATA_WebUIAutoTests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        [ThreadStatic]
        static EnvironmentDetails environmentDetails;
        //Global Variable for Extend report
        [ThreadStatic]
        private static ExtentTest featureName;
        [ThreadStatic]
        private static ExtentTest scenario;
        [ThreadStatic]
        private static ExtentReports extent;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
           using (StreamReader r = new StreamReader(Directory.GetCurrentDirectory() + "\\Environment.json"))
            {
             environmentDetails = JsonConvert.DeserializeObject<EnvironmentDetails>(r.ReadToEnd());
            }
            //Initialize Extent report before test starts
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Target" + DateTime.Now.ToString("-dd-MM-yyyy(hh-mm-ss)") + "\\index.html)";
            Directory.CreateDirectory(filePath);
            var htmlReporter = new ExtentHtmlReporter(filePath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //Attach report to reporter
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            LoggingLevelSwitch levelSwitch= new LoggingLevelSwitch(LogEventLevel.Debug);
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.ControlledBy(levelSwitch)
                         .WriteTo.File(filePath+@"\Logs",
                         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} |{Message} {NewLine}",
                         rollingInterval: RollingInterval.Day).CreateLogger();   
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            Log.Information("Selecting featur file {0} to run", FeatureContext.Current.FeatureInfo.Title);
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            Log.Information("Selecting Scenario  {0} to run", ScenarioContext.Current.ScenarioInfo.Title);
            BrowserFactory.InitBrowser(environmentDetails.BrowserName);
            BrowserFactory.LoadApplication(environmentDetails.AppURL);
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext sc)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(sc, null);

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass("pass", BrowserFactory.GetScreenshot());
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass("pass", BrowserFactory.GetScreenshot());
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass("pass", BrowserFactory.GetScreenshot());
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Pass("pass", BrowserFactory.GetScreenshot());
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                Log.Error("Test Step failed | ", ScenarioContext.Current.TestError.Message);
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
                else if (stepType == "When")
                {
                    string SSPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Screenshot\\";
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message, BrowserFactory.GetScreenshot());

                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);

                }
            }

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
            }

        }


        [AfterScenario]
        public void AfterScenario()
        {
            BrowserFactory.CloseAllDrivers();   
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();
        }
    }
}