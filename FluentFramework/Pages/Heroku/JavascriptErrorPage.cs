using FluentFramework.Core;

namespace FluentFramework.Pages.Heroku
{
    public class JavascriptErrorPage : BasePage
    {
        public JavascriptErrorPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                    .ClickLink("JavaScript onload event error");
            };
        }
    }
}
