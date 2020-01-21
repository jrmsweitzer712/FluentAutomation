using FluentFramework.Core;

namespace FluentFramework.Pages.Heroku
{
    public class LandingPage: BasePage
    {
        public LandingPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                var partial = "https://the-internet.herokuapp.com/";
                if (Driver.Url != partial)
                    I.GoToUrl(partial);
            };
        }

        public void ClickLink(string linkText)
        {
            I.Click(linkText);
        }
    }
}
