using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace FluentFramework.Core.Provider
{
    public interface IActionSyntaxProvider
    {
        /// <summary>
        /// Navigates to the given URL and logs to the logger.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        IActionSyntaxProvider GoToUrl(string url);

        /// <summary>
        /// Clicks a button or link by buttonOrLinkText, locator, or element
        /// </summary>
        /// <param name="buttonOrLinkText">The text of the button or link you want to click</param>
        /// <param name="by">The By locator of the element you want to click</param>
        /// <param name="element">The element to click</param>
        /// <param name="alias">A loggable, human-readable label for this button or link</param>
        /// <returns></returns>
        IActionSyntaxProvider Click(string buttonOrLinkText);
        IActionSyntaxProvider Click(By by);
        IActionSyntaxProvider Click(By by, string alias);
        IActionSyntaxProvider Click(IWebElement element);
        IActionSyntaxProvider Click(IWebElement element, string alias);
        /// <summary>
        /// Clicks first instance of a button or link by buttonOrLinkText, locator, or element
        /// </summary>
        /// <param name="buttonOrLinkText">The text of the button or link you want to click</param>
        /// <param name="by">The By locator of the element you want to click</param>
        /// <param name="element">The element to click</param>
        /// <param name="alias">A loggable, human-readable label for this button or link</param>
        /// <returns></returns>
        IActionSyntaxProvider ClickFirst(string buttonOrLinkText);
        IActionSyntaxProvider ClickFirst(By by);
        IActionSyntaxProvider ClickFirst(By by, string alias);
        IActionSyntaxProvider ClickFirst(IWebElement element);
        IActionSyntaxProvider ClickFirst(IWebElement element, string alias);
        /// <summary>
        /// Clicks a button or link by occurance on the page by buttonOrLinkText, locator, or element
        /// </summary>
        /// <param name="buttonOrLinkText">The text of the button or link you want to click</param>
        /// <param name="by">The By locator of the element you want to click</param>
        /// <param name="element">The element to click</param>
        /// <param name="alias">A loggable, human-readable label for this button or link</param>
        /// <param name="index">The occurance of the buttonOrLink on the page</param>
        /// <returns></returns>
        IActionSyntaxProvider ClickByIndex(string buttonOrLinkText, int index);
        IActionSyntaxProvider ClickByIndex(By by, int index);
        IActionSyntaxProvider ClickByIndex(By by, int index, string alias);
        IActionSyntaxProvider ClickByIndex(IWebElement element, int index);
        IActionSyntaxProvider ClickByIndex(IWebElement element, int index, string alias);

        /// <summary>
        /// Enters text into an input found by label, by locator, or element
        /// </summary>
        /// <param name="inputLabel">The label of the element you want to input into</param>
        /// <param name="by">The By locator of the element you want to input into</param>
        /// <param name="element">The element to input into</param>
        /// <param name="text">The text you want to input</param>
        /// <param name="alias">A loggable, human-readable label for this input</param>
        /// <returns></returns>
        IActionSyntaxProvider EnterText(string inputLabel, string text);
        IActionSyntaxProvider EnterText(By by, string text);
        IActionSyntaxProvider EnterText(By by, string text, string alias);
        IActionSyntaxProvider EnterText(IWebElement element, string text);
        IActionSyntaxProvider EnterText(IWebElement element, string text, string alias);

        /// <summary>
        /// Selects an option from a dropdown found by a label, by locator, or element
        /// </summary>
        /// <param name="dropdownLabel">The label of the dropdown to select</param>
        /// <param name="by">The By locator of the element you want to input into</param>
        /// <param name="element">The element to input into</param>
        /// <param name="optionText">The option you want to select</param>
        /// <param name="alias">A loggable, human-readable label for this dropdown</param>
        /// <returns></returns>
        IActionSyntaxProvider SelectFromDropdownByText(string dropdownLabel, string optionText);
        IActionSyntaxProvider SelectFromDropdownByText(By by, string optionText);
        IActionSyntaxProvider SelectFromDropdownByText(By by, string optionText, string alias);
        IActionSyntaxProvider SelectFromDropdownByText(IWebElement element, string optionText);
        IActionSyntaxProvider SelectFromDropdownByText(IWebElement element, string optionText, string alias);
               
        /// <summary>
        /// Selects a value from a dropdown found by a label, by locator, or element
        /// </summary>
        /// <param name="dropdownLabel">The label of the dropdown to select</param>
        /// <param name="by">The By locator of the element you want to input into</param>
        /// <param name="element">The element to input into</param>
        /// <param name="value">The value of the option you want to select</param>
        /// <param name="alias">A loggable, human-readable label for this dropdown</param>
        /// <returns></returns>
        IActionSyntaxProvider SelectFromDropdownByValue(string dropdownLabel, string value);
        IActionSyntaxProvider SelectFromDropdownByValue(By by, string value);
        IActionSyntaxProvider SelectFromDropdownByValue(By by, string value, string alias);
        IActionSyntaxProvider SelectFromDropdownByValue(IWebElement element, string value);
        IActionSyntaxProvider SelectFromDropdownByValue(IWebElement element, string value, string alias);

        /// <summary>
        /// Selects an index or value from a dropdown found by a label, by locator, or element
        /// </summary>
        /// <param name="dropdownLabel">The label of the dropdown to select</param>
        /// <param name="by">The By locator of the element you want to input into</param>
        /// <param name="element">The element to input into</param>
        /// <param name="optionIndex">The index of the option you want to select</param>
        /// <param name="alias">A loggable, human-readable label for this dropdown</param>
        /// <returns></returns>
        IActionSyntaxProvider SelectFromDropdownByIndex(string dropdownLabel, int optionIndex);
        IActionSyntaxProvider SelectFromDropdownByIndex(By by, int optionIndex);
        IActionSyntaxProvider SelectFromDropdownByIndex(By by, int optionIndex, string alias);
        IActionSyntaxProvider SelectFromDropdownByIndex(IWebElement element, int optionIndex);
        IActionSyntaxProvider SelectFromDropdownByIndex(IWebElement element, int optionIndex, string alias);

        /// <summary>
        /// Checks a checkbox found by a label, by, or element
        /// </summary>
        /// <param name="checkboxLabel">The label of the checkbox to check</param>
        /// <param name="by">The By locator of the checkbox you want to check</param>
        /// <param name="element">The element to check</param>
        /// <param name="alias">A loggable, human-readable label for this checkbox</param>
        /// <returns></returns>
        IActionSyntaxProvider CheckCheckbox(string checkboxLabel);
        IActionSyntaxProvider CheckCheckbox(By by);
        IActionSyntaxProvider CheckCheckbox(By by, string alias);
        IActionSyntaxProvider CheckCheckbox(IWebElement element);
        IActionSyntaxProvider CheckCheckbox(IWebElement element, string alias);

        /// <summary>
        /// Unchecks a checkbox found by a label, by, or element
        /// </summary>
        /// <param name="checkboxLabel">The label of the checkbox to uncheck</param>
        /// <param name="by">The By locator of the checkbox you want to uncheck</param>
        /// <param name="element">The element to uncheck</param>
        /// <param name="alias">A loggable, human-readable label for this checkbox</param>
        /// <returns></returns>
        IActionSyntaxProvider UncheckCheckbox(string checkboxLabel);
        IActionSyntaxProvider UncheckCheckbox(By by);
        IActionSyntaxProvider UncheckCheckbox(By by, string alias);
        IActionSyntaxProvider UncheckCheckbox(IWebElement element);
        IActionSyntaxProvider UncheckCheckbox(IWebElement element, string alias);

        /// <summary>
        /// Clicks the option text for the radio button found by a given label, by, or element
        /// </summary>
        /// <param name="radioLabel">The label of the radio button to select</param>
        /// <param name="by">The By locator of the radio you want to select</param>
        /// <param name="element">The element of the radio you want to select</param>
        /// <param name="option">The option you want to select</param>
        /// <param name="alias">A loggable, human-readable label for this checkbox</param>
        /// <returns></returns>
        IActionSyntaxProvider ClickRadioButton(string radioLabel, string option);
        IActionSyntaxProvider ClickRadioButton(By by, string option);
        IActionSyntaxProvider ClickRadioButton(By by, string option, string alias);
        IActionSyntaxProvider ClickRadioButton(IWebElement element, string option);
        IActionSyntaxProvider ClickRadioButton(IWebElement element, string option, string alias);

        /// <summary>
        /// Executes Javascript in the browser
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        IActionSyntaxProvider ExecuteJavascript(string js);
        /// <summary>
        /// Executes Javascript in the browser
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        T ExecuteJavascript<T>(string js);

        /// <summary>
        /// Scrolls to the top of the page 
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider ScrollToTop();
        /// <summary>
        /// Scrolls to the bottom of the page
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider ScrollToBottom();
        /// <summary>
        /// Scrolls to the element found by the given by, element, or coordinates
        /// </summary>
        /// <param name="by">The By locator to scroll to</param>
        /// <param name="element">The element to scroll to</param>
        /// <param name="x">The x-coordinate to scroll to</param>
        /// <param name="y">The y-coordinate to scroll to</param>
        /// <param name="alias">A loggable, human-readable label for this checkbox</param>
        /// <returns></returns>
        IActionSyntaxProvider ScrollTo(By by);
        IActionSyntaxProvider ScrollTo(IWebElement element);
        IActionSyntaxProvider ScrollTo(int x, int y);

        /// <summary>
        /// Takes a screenshot of the current page
        /// <param name="fileName">The name to give to the screenshot file</param>
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider TakeScreenshot();
        IActionSyntaxProvider TakeScreenshot(string fileName);

        /// <summary>
        /// Waits a given amount of time
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        IActionSyntaxProvider Wait(int milliseconds = 0, int seconds = 0);

        /// <summary>
        /// Waits until the given condition is true. Usage: I.WaitUntil(d => { return true; }, "This message is thrown if timeout exceeded", timeout)
        /// </summary>
        /// <param name="conditions">The conditional to wait for. This can be as complex as necessary.</param>
        /// <param name="failureMessage">The message that gets thrown if the conditional is not met.</param>
        /// <param name="timeout">The timeout for the wait.</param>
        /// <returns></returns>
        IActionSyntaxProvider WaitUntil(Func<IWebDriver, bool> conditions, string failureMessage);
        IActionSyntaxProvider WaitUntil(Func<IWebDriver, bool> conditions, string failureMessage, TimeSpan timeout);
        //IActionSyntaxProvider WaitUntil(Func<IWebDriver, IActionSyntaxProvider> conditions, string failureMessage);
        //IActionSyntaxProvider WaitUntil(Func<IWebDriver, IActionSyntaxProvider> conditions, string failureMessage, TimeSpan timeout);

        IActionSyntaxProvider WaitForReadyState();

        /// <summary>
        /// Finds an element by a given By locator.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        IWebElement Find(By by);
        /// <summary>
        /// Finds a list of elements by a given By locator.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        List<IWebElement> FindMultiple(By by);

        /// <summary>
        /// Switches driver to a given iframe
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        IActionSyntaxProvider SwitchToFrame(By by);
        /// <summary>
        /// Switches driver out of any iframes back to the main focus.
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider SwitchToMainContent();

        /// <summary>
        /// Returns a list of Querys for the webpage.
        /// </summary>
        IQuerySyntaxProvider Query { get; }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IActionSyntaxProvider LogMessage(string message);
        /// <summary>
        /// Logs a message with a timestamp
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IActionSyntaxProvider LogMessageWithTimestamp(string message);

        /// <summary>
        /// Hovers over an element found by a given By
        /// </summary>
        /// <param name="by"></param>
        /// <param name="alias">A loggable, human-readable label for this checkbox</param>
        /// <returns></returns>
        IActionSyntaxProvider Hover(By by);
        IActionSyntaxProvider Hover(By by, string alias);

        /// <summary>
        /// Refreshes the webpage
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider Refresh();

        /// <summary>
        /// Navigates back via the back button on the browser.
        /// </summary>
        /// <returns></returns>
        IActionSyntaxProvider NavigateBack();

        /// <summary>
        /// Returns if the given text is visible on the screen
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool SeeText(string text);

        /// <summary>
        /// Returns an element of the dropdown for a given label. 
        /// Used in Query.SelectedOption(string label)
        /// </summary>
        /// <param name="dropdownLabel"></param>
        /// <returns></returns>
        IWebElement FindDropdownByLabel(string dropdownLabel);
    }
}
