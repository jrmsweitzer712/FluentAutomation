using FluentFramework.Core.Exceptions;
using FluentFramework.Core.Extensions;
using FluentFramework.Core.Provider;
using FluentFramework.Pages.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Linq;

namespace FluentFramework.Core
{
    [TestClass]
    public class TestBase
    {
        public TestContext TestContext { get; set; }

        private Logger _logger;
        public Logger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = new Logger();
                return _logger;
            }
        }

        private IActionSyntaxProvider _i;
        public IActionSyntaxProvider I
        {
            get
            {
                if (_i == null)
                    _i = new ActionSyntaxProvider(this);
                return _i;
            }
        }

        private IWebDriver _driver;
        /// <summary>
        /// The WebDriver
        /// </summary>
        public IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                    _driver = CreateDriver(Settings.Browser);
                return _driver;
            }
            set
            {
                _driver = value;
            }
        }

        private Settings _settings;
        public Settings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new Settings();
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        public TNewPage SwitchTo<TNewPage>() where TNewPage : BasePage
        {
            var newPage = (TNewPage)Activator.CreateInstance(typeof(TNewPage), new object[] { this });
            if (newPage.Navigate != null)
            {
                try
                {
                    newPage.Navigate();
                }
                catch (ErrorPageException)
                {
                    throw;
                }
                catch (ConsoleErrorException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to navigate to the given page. See InnerException for details.", ex);
                }
            }
            if (newPage.OnLoad != null)
            {
                try
                {
                    newPage.OnLoad();
                }
                catch (ErrorPageException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }
            }

            if (newPage.GetType() != typeof(ErrorPage))
                SwitchTo<ErrorPage>();

            var consoleErrors = Driver.GetConsoleErrors();
            if (consoleErrors.Any())
                throw new ConsoleErrorException(string.Join(Environment.NewLine,consoleErrors));

            return newPage;
        }

        public TNewPage SwitchTo<TNewPage>(object param) where TNewPage : BasePage
        {
            var newPage = (TNewPage)Activator.CreateInstance(typeof(TNewPage), new object[] { this, param });
            if (newPage.Navigate != null)
            {
                try
                {
                    newPage.Navigate();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to navigate to the given page. See InnerException for details.", ex);
                }
            }
            if (newPage.OnLoad != null)
            {
                try
                {
                    newPage.OnLoad();
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }
            }

            return newPage;
        }


        [TestInitialize]
        public void TestSetup()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver = null;
        }
        

        /// <summary>
        /// Creates and returns the WebDriver for use.
        /// </summary>
        /// <param name="browserType"></param>
        /// <returns></returns>
        private IWebDriver CreateDriver(string browserType)
        {
            switch(browserType.ToLower())
            {
                case "ie":
                    return CreateIEDriver();
                default:
                    return CreateChromeDriver();
            }
        }

        /// <summary>
        /// Creates the Chrome Driver
        /// </summary>
        /// <param name="dir"></param>
        public IWebDriver CreateChromeDriver()
        {
            try
            {
                Logger.LogMessageWithTimestamp("Creating chrome driver...");
                var dir = Settings.DriverDirectory;
                var opt = new ChromeOptions();
                opt.SetLoggingPreference(LogType.Browser, LogLevel.All);
                opt.SetLoggingPreference(LogType.Driver, LogLevel.All);
                opt.SetLoggingPreference(LogType.Server, LogLevel.All);
                opt.SetLoggingPreference(LogType.Client, LogLevel.All);
                opt.SetLoggingPreference(LogType.Profiler, LogLevel.All);

                if (Settings.Mobile)
                {
                    //ChromeMobileEmulationDeviceSettings CMEDS = new ChromeMobileEmulationDeviceSettings();
                    //CMEDS.Width = 660;
                    //CMEDS.Height = 370;
                    //CMEDS.PixelRatio = 1.0;
                    //CMEDS.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_0 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10A5376e Safari/8536.25";
                    opt.EnableMobileEmulation("Galaxy S5");
                }

                opt.AddExtension(Settings.ExternalResources + "/JavascriptErrorsNotifier.crx");

                return new ChromeDriver(dir, opt);
            }
            catch (Exception ex)
            {
                Logger.LogMessageWithTimestamp("Could not create driver: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates the IE Driver
        /// </summary>
        /// <param name="dir"></param>
        public IWebDriver CreateIEDriver()
        {
            try
            {
                var dir = Settings.DriverDirectory;
                Logger.LogMessageWithTimestamp("Creating ie driver...");
                var options = new InternetExplorerOptions();
                options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                options.PageLoadStrategy = PageLoadStrategy.None;
                return new InternetExplorerDriver(dir, options);
            }
            catch (Exception ex)
            {
                Logger.LogMessageWithTimestamp("Could not create driver: " + ex.Message);
                throw;
            }
        }
    }
}
