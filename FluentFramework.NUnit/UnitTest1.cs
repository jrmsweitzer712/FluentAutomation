using FluentFramework.Core.Exceptions;
using FluentFramework.Pages.Heroku;
using NUnit.Framework;

namespace FluentFramework.NUnit
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class UnitTest1: NUnitTestBase
    {

        [Test]
        public void TestMethod1()
        {
            var cbPage = SwitchTo<CheckboxesPage>();

            cbPage.CheckCheckbox(1);
            cbPage.CheckCheckbox(2);

            Assert.IsTrue(cbPage.GetChecked(1));
            Assert.IsTrue(cbPage.GetChecked(2));

            cbPage.CheckCheckbox(1, false);
            cbPage.CheckCheckbox(2, false);

            Assert.IsFalse(cbPage.GetChecked(1));
            Assert.IsFalse(cbPage.GetChecked(2));
        }

        [Test]
        public void Dropdown()
        {
            var ddPage = SwitchTo<DropdownPage>();

            ddPage.SelectOptionByIndex(1);
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 1");
            ddPage.SelectOptionByIndex(2);
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 2");

            ddPage.SelectOptionByText("Option 1");
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 1");
            ddPage.SelectOptionByText("Option 2");
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 2");

            ddPage.SelectOptionByValue("1");
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 1");
            ddPage.SelectOptionByValue("2");
            Assert.IsTrue(ddPage.GetSelectedText() == "Option 2");
        }

        [Test]
        public void FormAuthentication()
        {
            var loginPage = SwitchTo<FormAuthenticationPage>();
            loginPage.Login("tomsmith", "SuperSecretPassword!");

            Assert.IsTrue(I.SeeText("You logged into a secure area!"));
            Assert.IsTrue(Driver.Url.Contains("/secure"));
        }

        [Test]
        public void SlowResources()
        {
            // This test does not currently function as expected.
            // The Slow Resources page currently returns a 503 (Service Unavailable)
            // on /slow_external.
            var page = SwitchTo<SlowResourcesPage>();

            // TODO: Update test to check for 503 Service Unavailable in the Console logs.

        }

        [Test]
        public void WYSIWYGEditor()
        {
            var wysiwyg = SwitchTo<WYSIWYGPage>();

            // TODO: Update test to use the WYSIWYG Editor
        }

        [Test]
        public void IE()
        {
            Settings.Browser = "IE";

            var wysiwyg = SwitchTo<WYSIWYGPage>();

            Logger.LogMessage("IE");
        }

        [Test]
        public void Mobile()
        {
            Settings.Mobile = true;

            var wysiwyg = SwitchTo<WYSIWYGPage>();

            Logger.LogMessage("Mobile");
        }

        [Test]
        public void ErrorPageShouldThrow()
        {
            var scPage = SwitchTo<StatusCodePage>();

            Assert.Throws<ErrorPageException>(
                () => scPage.ClickLink("404"));
        }

        [Test]
        public void JavascriptOnloadEventError()
        {
            Assert.Throws<ConsoleErrorException>(
                () => SwitchTo<JavascriptErrorPage>());
        }
    }
}
