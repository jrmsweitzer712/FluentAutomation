using FluentFramework.Core;
using FluentFramework.Pages.Heroku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace FluentFramework.SF.Steps
{
    [Binding]
    public sealed class HerokuAppSteps: SpecFlowTestBase
    {
        public HerokuAppSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Given(@"I have navigated to the HerokuApp main page")]
        public void GivenIHaveNavigatedToTheHerokuAppMainPage()
        {
            SwitchTo<LandingPage>();
        }

        [When(@"I click link (.*)")]
        public void WhenIClickLink(string linkText)
        {
            I.Click(linkText);
        }

        [Then(@"I should see URL partial (.*)")]
        public void ThenIShouldSeeURLPartial(string partial)
        {
            Assert.IsTrue(Driver.Url.Contains(partial));
        }

    }
}
