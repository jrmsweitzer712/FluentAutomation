using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace FluentFramework.Core.Extensions
{
    public static class IWebDriverExtensions
    {
        public static string VisibleText(this IWebDriver driver)
        {
            return driver.FindElement(By.TagName("html")).Text;
        }

        public static List<string> GetConsoleErrors(this IWebDriver driver)
        {
            try
            {
                var logTypes = driver.Manage().Logs.AvailableLogTypes.ToList();

                return logTypes;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
