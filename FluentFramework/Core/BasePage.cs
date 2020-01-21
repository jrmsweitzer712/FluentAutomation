using FluentFramework.Core.Provider;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace FluentFramework.Core
{
    public class BasePage
    {
        /// <summary>
        /// Default Cobnstructor
        /// </summary>
        public BasePage(TestBase test)
        {
            _test = test;
            I = new ActionSyntaxProvider(_test);
        }

        /// <summary>
        /// The WebDriver
        /// </summary>
        protected IWebDriver Driver => _test.Driver;

        /// <summary>
        /// The Settings file
        /// </summary>
        protected Settings Settings => _test.Settings;

        /// <summary>
        /// One of the methods that gets called automatically when you SwitchTo<> a page. 
        /// Use this method to define navigation to a page.
        /// </summary>
        internal Action Navigate { get; set; }

        /// <summary>
        /// One of the methods that gets called automatically when you SwitchTo<> a page.
        /// Use this method to define checks for when a page loads (WaitFor, assert Element is visible, etc). 
        /// </summary>
        internal Action OnLoad { get; set; }

        /// <summary>
        /// The method you call when you want to send control of selenium to a new page.
        /// </summary>
        /// <typeparam name="TNewPage"></typeparam>
        /// <returns></returns>
        protected TNewPage SwitchTo<TNewPage>() where TNewPage : BasePage => _test.SwitchTo<TNewPage>();
             
        /// <summary>
        /// The tool to do most of our work
        /// </summary>
        protected IActionSyntaxProvider I;

        private TestBase _test;
    }
}
