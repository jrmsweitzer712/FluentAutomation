using FluentFramework.Core;

namespace FluentFramework.Pages.Heroku
{
    public class FormAuthenticationPage : BasePage
    {
        public FormAuthenticationPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                .ClickLink("Form Authentication");
            };
        }

        public void Login(string username, string password)
        {
            I.EnterText("Username", username);
            I.EnterText("Password", password);
            I.Click("Login");

            I.WaitUntil(d =>
            {
                return I.SeeText("You logged into a secure area!") ||
                I.SeeText("Your password is invalid!") ||
                I.SeeText("Your username is invalid!");
            }, "Clicked login, timed out.");
        }
    }
}
