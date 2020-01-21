using OpenQA.Selenium;
using System.Collections.Generic;

namespace FluentFramework.Core.Provider
{
    public interface IQuerySyntaxProvider
    {
        /// <summary>
        /// Returns true if the element exists in the DOM
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        bool Exists(By by);

        /// <summary>
        /// Returns true if the element is Displayed
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        bool ElementIsVisible(By by);

        /// <summary>
        /// Returns true if the element is enabled.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        bool ElementIsEnabled(By by);

        /// <summary>
        /// Returns the text of an element found by a given locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        string Text(By by);

        /// <summary>
        /// Returns a list containing the text of all elements found by a given locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        List<string> TextOfMultiple(By by);

        /// <summary>
        /// Returns the value of an element found by a given locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        string Value(By by);

        /// <summary>
        /// Returns a list containing the values of all elements found by a given locator
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        List<string> ValueOfMultiple(By by);

        /// <summary>
        /// Returns the selected option text of a dropdown found by a given locator.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        string SelectedOption(string dropdownLabel);
        string SelectedOption(By by);

        /// <summary>
        /// Returns a list containing the text of all options of an element found by a given locator.
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        List<string> Options(By by);

        /// <summary>
        /// Returns true if the checkbox found by the given locator or label is checked
        /// </summary>
        /// <param name="checkboxLabel"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        bool Checked(string checkboxLabel);
        bool Checked(By by);
        bool Checked(IWebElement element);

        /// <summary>
        /// Returns true if an alert is visible on the screen.
        /// </summary>
        /// <returns></returns>
        bool AlertExists();

        /// <summary>
        /// Returns the message of a visible alert.
        /// </summary>
        /// <returns></returns>
        string AlertMessage();
    }
}
