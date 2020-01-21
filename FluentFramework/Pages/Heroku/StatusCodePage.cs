using FluentFramework.Core;

namespace FluentFramework.Pages.Heroku
{
    public class StatusCodePage : BasePage
    {
        public StatusCodePage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                    .ClickLink("Status Codes");
            };
        }

        public void ClickLink(string linkText)
        {
            I.Click(linkText);
        }
    }
}
