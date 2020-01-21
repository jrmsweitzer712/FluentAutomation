using FluentFramework.Core;
using FluentFramework.Core.Exceptions;
using FluentFramework.Pages;
using FluentFramework.Pages.Heroku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace FluentFramework.Tests
{
    [TestClass]
    public class Tests: TestBase
    {
        [TestMethod]
        public void Checkboxes()
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

        [TestMethod]
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

        [TestMethod]
        public void FormAuthentication()
        {
            var loginPage = SwitchTo<FormAuthenticationPage>();
            loginPage.Login("tomsmith", "SuperSecretPassword!");

            Assert.IsTrue(I.SeeText("You logged into a secure area!"));
            Assert.IsTrue(Driver.Url.Contains("/secure"));
        }

        [TestMethod]
        public void SlowResources()
        {
            // This test does not currently function as expected.
            // The Slow Resources page currently returns a 503 (Service Unavailable)
            // on /slow_external.
            var page = SwitchTo<SlowResourcesPage>();

            // TODO: Update test to check for 503 Service Unavailable in the Console logs.

        }

        [TestMethod]
        public void WYSIWYGEditor()
        {
            var wysiwyg = SwitchTo<WYSIWYGPage>();

            // TODO: Update test to use the WYSIWYG Editor
        }

        [TestMethod]
        public void IE()
        {
            Settings.Browser = "IE";

            var wysiwyg = SwitchTo<WYSIWYGPage>();

            Logger.LogMessage("IE");
        }

        [TestMethod]
        public void Mobile()
        {
            Settings.Mobile = true;

            var wysiwyg = SwitchTo<WYSIWYGPage>();

            Logger.LogMessage("Mobile");
        }

        [TestMethod]
        public void ErrorPageShouldThrow()
        {
            var scPage = SwitchTo<StatusCodePage>();

            Assert.ThrowsException<ErrorPageException>(
                () => scPage.ClickLink("404"));
        }

        [TestMethod]
        public void JavascriptOnloadEventError()
        {
            Assert.ThrowsException<ConsoleErrorException>(
                () => SwitchTo<JavascriptErrorPage>());
        }
    }
}
