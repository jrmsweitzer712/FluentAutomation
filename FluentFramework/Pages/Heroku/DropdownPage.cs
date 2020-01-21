using FluentFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFramework.Pages.Heroku
{
    public class DropdownPage : BasePage
    {
        public DropdownPage(TestBase test) : base(test)
        {
            Navigate = () =>
            {
                SwitchTo<LandingPage>()
                    .ClickLink("Dropdown");
            };
        }

        public void SelectOptionByText(string text)
        {
            I.SelectFromDropdownByText("Dropdown List", text);
        }

        public void SelectOptionByValue(string value)
        {
            I.SelectFromDropdownByValue("Dropdown List", value);
        }

        public void SelectOptionByIndex(int index)
        {
            I.SelectFromDropdownByIndex("Dropdown List", index);
        }

        public string GetSelectedText()
        {
            return I.Query.SelectedOption("Dropdown List");
        }
    }
}
