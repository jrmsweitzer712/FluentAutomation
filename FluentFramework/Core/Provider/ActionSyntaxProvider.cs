using FluentFramework.Core.Exceptions;
using FluentFramework.Pages.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FluentFramework.Core.Provider
{
    public class ActionSyntaxProvider: IActionSyntaxProvider
    {
        private IWebDriver Driver => Test.Driver;
        private Settings Settings => Test.Settings;
        private Logger Logger => Test.Logger;
        private TestBase Test { get; }
        private IActionSyntaxProvider I => this;

        public ActionSyntaxProvider(TestBase test)
        {
            Test = test;
        }

        public IQuerySyntaxProvider Query => new QuerySyntaxProvider(Driver, this);

        public IActionSyntaxProvider GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Logger.LogMessageWithTimestamp($"Navigated to URL: '{url}'");
            Logger.LogMessageWithTimestamp(Constants.LineDivider);
            return this;
        }

        public IActionSyntaxProvider Click(string buttonOrLinkText)
        {
            var potentials = new List<By>()
            {
                // Exact text
                By.LinkText(buttonOrLinkText),
                By.XPath($"//button[.='{buttonOrLinkText}']"),
                By.XPath($"//button[@value='{buttonOrLinkText}']"),
                By.XPath($"//input[@type='button' and .='{buttonOrLinkText}']"),
                By.XPath($"//input[@type='button' and @value='{buttonOrLinkText}']"),

                // Contains
                By.PartialLinkText(buttonOrLinkText),
                By.XPath($"//button[contains(.,'{buttonOrLinkText}')]"),
                By.XPath($"//button[contains(@value,'{buttonOrLinkText}')]"),
                By.XPath($"//input[@type='button' and contains(.,'{buttonOrLinkText}')]"),
                By.XPath($"//input[@type='button' and contains(@value,'{buttonOrLinkText}')]"),
            };

            I.WaitUntil(d =>
            {
                foreach (var potential in potentials)
                {
                    if (I.Query.Exists(potential) && I.Query.ElementIsVisible(potential))
                    {
                        if (I.FindMultiple(potential).Count > 1)
                            throw new Exception($"Multiple elements found with label '{buttonOrLinkText}'. Please use I.ClickFirst or I.ClickByIndex instead.");

                        I.Click(I.Find(potential), buttonOrLinkText);
                        return true;
                    }
                }
                return false;
            }, $"Could not find a button or link with text {buttonOrLinkText} within the timeout");

            AfterEvent();

            return this;
        }

        private void AfterEvent()
        {
            I.WaitForReadyState();
            I.Wait(500);
            CheckForJavascriptErrors();
        }

        public IActionSyntaxProvider WaitForReadyState()
        {
            I.WaitUntil(d =>
            {
                string readyState = I.ExecuteJavascript<string>("return document.readyState");

                return readyState == "complete";
            }, "Document was not ready in the given timeout");

            return this;
        }

        public IActionSyntaxProvider ExecuteJavascript(string javascript)
        {
            var ijsExecutor = (IJavaScriptExecutor)Driver;
            ijsExecutor.ExecuteScript(javascript);
            return this;
        }

        public T ExecuteJavascript<T>(string javascript)
        {
            var ijsExecutor = (IJavaScriptExecutor)Driver;
            return ((T)(ijsExecutor.ExecuteScript(javascript)));
        }

        private void CheckForJavascriptErrors()
        {
            var jsErrorNotifierLocator = By.XPath("//img[@title='Some errors occurred on this page. Click to see details.']");

            if (I.Query.Exists(jsErrorNotifierLocator))
            {
                I.Find(jsErrorNotifierLocator).Click();

                var jsErroriFrame = By.XPath("//iframe[contains(@src,'javascript_error')]");
                I.SwitchToFrame(jsErroriFrame);

                var text = I.Query.Text(By.TagName("html"));

                I.SwitchToMainContent();
                throw new ConsoleErrorException("Unexpected javascript on the page. " + text);
            }
        }

        public IActionSyntaxProvider Click(By by) => Click(Find(by), string.Empty);
        public IActionSyntaxProvider Click(By by, string alias) => Click(Find(by), alias);
        public IActionSyntaxProvider Click(IWebElement element) => Click(element, string.Empty);
        public IActionSyntaxProvider Click(IWebElement element, string alias)
        {
            I.WaitUntil(d => element.Displayed && element.Enabled,
                "Element was not visible or enabled to click");
            string urlBeforeClick = Driver.Url;
            element.Click();
            var al = string.IsNullOrEmpty(alias) ?
                string.IsNullOrEmpty(element.Text) ?
                    element.GetAttribute("value"):
                    element.Text :
                alias;
            I.LogMessageWithTimestamp($"Clicked button or link with text '{al}'");

            AfterClick(urlBeforeClick);

            return this;
        }

        private void AfterClick(string urlBeforeClick)
        {
            if (I.Query.AlertExists())
                return;

            if (Driver.Url != urlBeforeClick)
            {
                Logger.LogMessageWithTimestamp(Constants.LineDivider);
                Logger.LogMessageWithTimestamp("New URL: " + Driver.Url);
                Logger.LogMessageWithTimestamp(Constants.LineDivider);
            }

            Test.SwitchTo<ErrorPage>();
        }

        public IActionSyntaxProvider EnterText(string inputLabel, string text)
        {
            var potentials = new List<By>()
            {
                // Exact text
                By.XPath($"//label[.='{inputLabel}']/following::input[not(@type='hidden')][1]"),

                // Contains
                By.XPath($"//label[contains(.,'{inputLabel}')]/following::input[not(@type='hidden')][1]"),
            };

            I.WaitUntil(d =>
            {
                foreach (var potential in potentials)
                {
                    if (I.Query.Exists(potential) && I.FindMultiple(potential).Count == 1)
                    {
                        EnterText(potential, text, inputLabel);
                        return true;
                    }
                }

                return false;
            }, "Timed out looking for input with label " + inputLabel);

            return this;
        }

        public IActionSyntaxProvider EnterText(By by, string text) => EnterText(by, text, string.Empty);
        public IActionSyntaxProvider EnterText(By by, string text, string alias) => EnterText(I.Find(by), text, alias);
        public IActionSyntaxProvider EnterText(IWebElement element, string text) => EnterText(element, text, string.Empty);
        public IActionSyntaxProvider EnterText(IWebElement element, string text, string alias)
        {
            I.WaitUntil(d => element.Displayed && element.Enabled,
                "Element was not visible or enabled to input text");
            element.SendKeys(text);

            var al = string.IsNullOrEmpty(alias) ?
                string.IsNullOrEmpty(element.Text) ?
                    element.GetAttribute("value") :
                    element.Text :
                alias;
            I.LogMessageWithTimestamp($"Entered text {text} in '{al}' input.");
            return this;
        }

        public IActionSyntaxProvider SelectFromDropdownByText(string dropdownLabel, string optionText)
        {
            var element = FindDropdownByLabel(dropdownLabel);
            return SelectFromDropdownByText(element, optionText, dropdownLabel);
        }
        public IActionSyntaxProvider SelectFromDropdownByText(By by, string optionText) => SelectFromDropdownByText(by, optionText, string.Empty);
        public IActionSyntaxProvider SelectFromDropdownByText(By by, string optionText, string alias) => SelectFromDropdownByText(I.Find(by), optionText, alias);
        public IActionSyntaxProvider SelectFromDropdownByText(IWebElement element, string optionText) => SelectFromDropdownByText(element, optionText, string.Empty);
        public IActionSyntaxProvider SelectFromDropdownByText(IWebElement element, string optionText, string alias)
        {
            var select = new SelectElement(element);
            select.SelectByText(optionText);
            Logger.LogMessageWithTimestamp($"Selected option '{optionText}' from dropdown '{alias}'");
            return this;
        }

        public IActionSyntaxProvider SelectFromDropdownByValue(string dropdownLabel, string value)
        {
            var element = FindDropdownByLabel(dropdownLabel);
            return SelectFromDropdownByValue(element, value, dropdownLabel);
        }
        public IActionSyntaxProvider SelectFromDropdownByValue(By by, string value) => SelectFromDropdownByValue(by, value, string.Empty);
        public IActionSyntaxProvider SelectFromDropdownByValue(By by, string value, string alias) => SelectFromDropdownByValue(I.Find(by), value, alias);
        public IActionSyntaxProvider SelectFromDropdownByValue(IWebElement element, string value) => SelectFromDropdownByValue(element, value, string.Empty);
        public IActionSyntaxProvider SelectFromDropdownByValue(IWebElement element, string value, string alias)
        {
            var select = new SelectElement(element);
            select.SelectByValue(value);
            Logger.LogMessageWithTimestamp($"Selected value '{value}' from dropdown '{alias}'");
            return this;
        }

        public IActionSyntaxProvider SelectFromDropdownByIndex(string dropdownLabel, int optionIndex)
        {
            var element = FindDropdownByLabel(dropdownLabel);
            return SelectFromDropdownByIndex(element, optionIndex, dropdownLabel);
        }
        public IActionSyntaxProvider SelectFromDropdownByIndex(By by, int optionIndex) => SelectFromDropdownByIndex(by, optionIndex);
        public IActionSyntaxProvider SelectFromDropdownByIndex(By by, int optionIndex, string alias) => SelectFromDropdownByIndex(Find(by), optionIndex, alias);
        public IActionSyntaxProvider SelectFromDropdownByIndex(IWebElement element, int optionIndex) => SelectFromDropdownByIndex(element, optionIndex, string.Empty);
        public IActionSyntaxProvider SelectFromDropdownByIndex(IWebElement element, int optionIndex, string alias)
        {
            var select = new SelectElement(element);
            select.SelectByIndex(optionIndex);
            Logger.LogMessageWithTimestamp($"Selected index '{optionIndex}' from dropdown '{alias}'");
            return this;
        }

        public IWebElement FindDropdownByLabel(string label)
        {
            var potentials = new List<By>()
            {
                By.XPath($"//label[.='{label}']/following::select[1]"),
                By.XPath($"//label[contains(.,'{label}')]/following::select[1]"),

                By.XPath($"//h3[.='{label}']/following::select[1]"),
            };

            IWebElement dropdown = null;

            I.WaitUntil(d =>
            {
                foreach (var potential in potentials)
                {
                    if (I.Query.Exists(potential) && I.FindMultiple(potential).Count == 1)
                    {
                        dropdown = I.Find(potential);
                        return true;
                    }
                };
                return false;
            }, "Timed out looking for selct with label: " + label);

            return dropdown;
        }

        public IActionSyntaxProvider CheckCheckbox(string checkboxLabel)
        {
            throw new NotImplementedException();
        }
        public IActionSyntaxProvider CheckCheckbox(By by) => CheckCheckbox(by, string.Empty);
        public IActionSyntaxProvider CheckCheckbox(By by, string alias) => CheckCheckbox(Find(by), alias);
        public IActionSyntaxProvider CheckCheckbox(IWebElement element) => CheckCheckbox(element, string.Empty);
        public IActionSyntaxProvider CheckCheckbox(IWebElement element, string alias)
        {
            if (Query.Checked(element) == false)
            {
                element.Click();
                Logger.LogMessageWithTimestamp($"Checked checkbox '{(string.IsNullOrEmpty(alias) ? string.Empty : alias)}'");
            }
            return this;
        }

        public IActionSyntaxProvider UncheckCheckbox(string checkboxLabel)
        {
            throw new NotImplementedException();
        }
        public IActionSyntaxProvider UncheckCheckbox(By by) => UncheckCheckbox(by, string.Empty);
        public IActionSyntaxProvider UncheckCheckbox(By by, string alias) => UncheckCheckbox(Find(by), alias);
        public IActionSyntaxProvider UncheckCheckbox(IWebElement element) => UncheckCheckbox(element, string.Empty);
        public IActionSyntaxProvider UncheckCheckbox(IWebElement element, string alias)
        {
            if (Query.Checked(element))
            {
                element.Click();
                Logger.LogMessageWithTimestamp($"Unchecked checkbox '{(string.IsNullOrEmpty(alias) ? string.Empty : alias)}'");
            }
            return this;
        }

        public IActionSyntaxProvider ClickRadioButton(string radioLabel, string option)
        {
            throw new NotImplementedException();
        }
        public IActionSyntaxProvider ClickRadioButton(By by, string option) => ClickRadioButton(by, option, string.Empty);
        public IActionSyntaxProvider ClickRadioButton(By by, string option, string alias) => ClickRadioButton(Find(by), option, alias);
        public IActionSyntaxProvider ClickRadioButton(IWebElement element, string option) => ClickRadioButton(element, option, string.Empty);
        public IActionSyntaxProvider ClickRadioButton(IWebElement element, string option, string alias)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ScrollToTop()
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ScrollToBottom()
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ScrollTo(By by)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ScrollTo(IWebElement element)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ScrollTo(int x, int y)
        {
            throw new NotImplementedException();
        }

        public IWebElement Find(By by)
        {
            I.WaitUntil(d => I.Query.ElementIsEnabled(by),
                $"Could not find element {by} within the timeout");

            return Driver.FindElement(by);
        }

        public List<IWebElement> FindMultiple(By by)
        {
            I.WaitUntil(d => I.Query.ElementIsEnabled(by),
                $"Could not find element {by} within the timeout");

            return Driver.FindElements(by).ToList();
        }

        public IActionSyntaxProvider TakeScreenshot()
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider TakeScreenshot(string fileName)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider Wait(int milliseconds = 0, int seconds = 0)
        {
            Thread.Sleep(seconds * 1000 + milliseconds);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Func<IWebDriver, bool> conditions, string failureMessage) => WaitUntil(conditions, failureMessage, Settings.CommandTimeout);
        public IActionSyntaxProvider WaitUntil(Func<IWebDriver, bool> conditions, string failureMessage, TimeSpan timeout)
        {
            try
            {
                new WebDriverWait(Driver, timeout).Until(conditions);
            }
            catch (ErrorPageException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(failureMessage, ex);
            }
            return this;
        }
        //public IActionSyntaxProvider WaitUntil(Func<IWebDriver, IActionSyntaxProvider> conditions, string failureMessage) => WaitUntil(conditions, failureMessage, Settings.CommandTimeout);
        //public IActionSyntaxProvider WaitUntil(Func<IWebDriver, IActionSyntaxProvider> conditions, string failureMessage, TimeSpan timeout)
        //{
        //    try
        //    {
        //        new WebDriverWait(Driver, timeout).Until(conditions);
        //    }
        //    catch (ErrorPageException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(failureMessage, ex);
        //    }
        //    return this;
        //}

        public IActionSyntaxProvider LogMessage(string message)
        {
            Logger.LogMessage(message);
            return this;
        }

        public IActionSyntaxProvider LogMessageWithTimestamp(string message)
        {
            Logger.LogMessageWithTimestamp(message);
            return this;
        }

        public IActionSyntaxProvider ClickFirst(string buttonOrLinkText)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ClickFirst(By by) => ClickFirst(by, string.Empty);
        public IActionSyntaxProvider ClickFirst(By by, string alias) => ClickFirst(Find(by), alias);
        public IActionSyntaxProvider ClickFirst(IWebElement element) => ClickFirst(element, string.Empty);
        public IActionSyntaxProvider ClickFirst(IWebElement element, string alias)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ClickByIndex(string buttonOrLinkText, int index)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider ClickByIndex(By by, int index) => ClickByIndex(by, index, string.Empty);
        public IActionSyntaxProvider ClickByIndex(By by, int index, string alias) => ClickByIndex(Find(by), index, alias);
        public IActionSyntaxProvider ClickByIndex(IWebElement element, int index) => ClickByIndex(element, index, string.Empty);
        public IActionSyntaxProvider ClickByIndex(IWebElement element, int index, string alias)
        {
            throw new NotImplementedException();
        }

        public IActionSyntaxProvider SwitchToFrame(By by)
        {
            var frame = Find(by);
            Driver.SwitchTo().Frame(frame);
            LogMessageWithTimestamp("Switched driver to an iframe.");
            return this;
        }

        public IActionSyntaxProvider SwitchToMainContent()
        {
            Driver.SwitchTo().DefaultContent();
            LogMessageWithTimestamp("Switched driver out of any iframes.");
            return this;
        }

        public IActionSyntaxProvider Hover(By by) => Hover(by, Query.Text(by));
        public IActionSyntaxProvider Hover(By by, string alias)
        {
            new Actions(Driver).MoveToElement(Find(by)).Build().Perform();
            LogMessageWithTimestamp($"Hovered over element {by}.");
            return this;
        }

        public IActionSyntaxProvider Refresh()
        {
            Driver.Navigate().Refresh();
            LogMessageWithTimestamp("Refreshed the page.");
            return this;
        }

        public IActionSyntaxProvider NavigateBack()
        {
            Driver.Navigate().Back();
            LogMessageWithTimestamp("Navigated back using the browser back button.");
            return this;
        }

        public bool SeeText(string text)
        {
            return Find(By.TagName("html")).Text.Contains(text);
        }
    }
}
