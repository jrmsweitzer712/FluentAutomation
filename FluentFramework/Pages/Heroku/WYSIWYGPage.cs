using FluentFramework.Core;

namespace FluentFramework.Pages.Heroku
{
    public class WYSIWYGPage : BasePage
    {
        public WYSIWYGPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                    .ClickLink("WYSIWYG");
            };
        }
    }
}
