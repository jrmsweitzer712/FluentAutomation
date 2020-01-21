using FluentFramework.Core;
using System.IO;
using TechTalk.SpecFlow;

namespace FluentFramework.SF.Hooks
{
    [Binding]
    public class Hooks : TestBase
    {
        private ScenarioContext _scenarioContext { get; set; }
        private FeatureContext _featureContext {get;set;}
        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeScenario]
        public void BeforeScenario()
        {
            base.TestSetup();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            base.Cleanup();
            //Driver.Quit();
            //Driver = null;
            //var testName = GetTestMethodName();
            //var dir = Settings.SeleniumDirectory + "/Reports/";
            //if (Directory.Exists(dir) == false)
            //    Directory.CreateDirectory(dir);

            //var filename = dir + testName + ".txt";
            //Logger.Publish(filename);
        }

        private string GetTestMethodName()
        {
            try
            {
                return _scenarioContext.ScenarioInfo.Title
                    .Replace("(", "")
                    .Replace(")", "")
                    .Replace("  ", " ")
                    .Replace(" ", "");
            }
            catch
            {
                throw;
            }
        }
    }
}
