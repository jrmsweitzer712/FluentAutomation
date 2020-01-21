using FluentFramework.Core;
using OpenQA.Selenium;
using System;

namespace FluentFramework.Pages.Heroku
{
    public class CheckboxesPage : BasePage
    {
        public CheckboxesPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                    .ClickLink("Checkboxes");
            };
        }

        By checkboxLocator(int index) => By.XPath($"//input[@type='checkbox'][{index}]");

        public void CheckCheckbox(int index, bool check = true)
        {
            if (check)
            {
                if (index == 1)
                    I.CheckCheckbox(checkboxLocator(1), "checkbox 1");
                else if (index == 2)
                    I.CheckCheckbox(checkboxLocator(2), "checkbox 2");
                else
                    throw new Exception("Index only accepts 1 or 2");
            }
            else
            {
                if (index == 1)
                    I.UncheckCheckbox(checkboxLocator(1), "checkbox 1");
                else if (index == 2)
                    I.UncheckCheckbox(checkboxLocator(2), "checkbox 2");
                else
                    throw new Exception("Index only accepts 1 or 2");
            }
        }

        public bool GetChecked(int index)
        {
            if (index == 1)
                return I.Query.Checked(checkboxLocator(1));
            else if (index == 2)
                return I.Query.Checked(checkboxLocator(2));
            else
                throw new Exception("Index only accepts 1 or 2");
        }
    }
}
