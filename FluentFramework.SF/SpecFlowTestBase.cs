using FluentFramework.Core;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace FluentFramework.SF
{
    public class SpecFlowTestBase : TestBase
    {
        public ScenarioContext ScenarioContext { get; set; }

        public SpecFlowTestBase(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var testName = GetTestMethodName();
            if (!string.IsNullOrEmpty(testName))
            {
                var dir = Settings.SeleniumDirectory + "/Reports/";
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);

                var filename = dir + testName + ".txt";
                Logger.Publish(filename);
            }

            base.Cleanup();
        }

        private string GetTestMethodName()
        {
            if (ScenarioContext != null)
            {
                try
                {
                    return ScenarioContext.ScenarioInfo.Title
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
            else
            {
                throw new Exception("Cannot find ScenarioContext.");
            }
        }
    }
}
