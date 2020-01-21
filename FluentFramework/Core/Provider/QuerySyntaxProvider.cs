using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentFramework.Core.Provider
{
    public class QuerySyntaxProvider: IQuerySyntaxProvider
    {
        private IWebDriver Driver;
        private IActionSyntaxProvider I;

        public QuerySyntaxProvider(IWebDriver driver, IActionSyntaxProvider i)
        {
            Driver = driver;
            I = i;
        }

        public bool Checked(string checkboxLabel)
        {
            throw new NotImplementedException();
        }

        public bool Checked(By by) => Checked(I.Find(by));
        public bool Checked(IWebElement element)
        {
            return element.GetAttribute("checked") != null;
        }

        public bool ElementIsEnabled(By by)
        {
            return Exists(by) && ElementIsVisible(by) &&
                Driver.FindElement(by).Enabled;
        }

        public bool ElementIsVisible(By by)
        {
            return Exists(by) && Driver.FindElements(by).Any(element => element.Displayed);
        }

        public bool Exists(By by)
        {
            try
            {
                return Driver.FindElement(by) != null;
            }
            catch
            {
                return false;
            }
        }

        public List<string> Options(By by)
        {
            throw new NotImplementedException();
        }

        public string SelectedOption(string label)
        {
            var select = new SelectElement(I.FindDropdownByLabel(label));
            return select.SelectedOption.Text;
        }
        public string SelectedOption(By by)
        {
            var select = new SelectElement(I.Find(by));
            return select.SelectedOption.Text;
        }

        public string Text(By by)
        {
            return I.Find(by).Text;
        }

        public List<string> TextOfMultiple(By by)
        {
            return I.FindMultiple(by).Select(element => element.Text).ToList();
        }

        public string Value(By by)
        {
            return I.Find(by).GetAttribute("value");
        }

        public List<string> ValueOfMultiple(By by)
        {
            return I.FindMultiple(by).Select(element => element.GetAttribute("value")).ToList();
        }

        public bool AlertExists()
        {
            try
            {
                Driver.SwitchTo().Alert();
                return true;
            }    
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
            finally
            {
                Driver.SwitchTo().DefaultContent();
            }
        }

        public string AlertMessage()
        {
            if (AlertExists() == false)
                throw new Exception("Queried for an alert message, but there is no alert on the screen.");

            try
            {
                var alert = Driver.SwitchTo().Alert();
                return alert.Text;
            }
            catch (NoAlertPresentException Ex)
            {
                return string.Empty;
            }
            finally
            {
                Driver.SwitchTo().DefaultContent();
            }
        }
    }
}
