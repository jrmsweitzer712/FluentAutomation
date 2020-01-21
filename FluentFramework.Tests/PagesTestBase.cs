using FluentFramework.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FluentFramework.Tests
{
    public class PagesTestBase : TestBase
    {
        [TestCleanup]
        public void AfterTest()
        {
            var testName = GetTestMethodName();
            if (!string.IsNullOrEmpty(testName))
            {
                var dir = Settings.SeleniumDirectory + "/Reports/";
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);

                var filename = dir + testName + ".txt";
                Logger.Publish(filename);
            }
            base.Cleanup();
        }

        private string GetTestMethodName()
        {
            if (TestContext != null)
            {
                try
                {
                    return TestContext.TestName
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("  ", " ")
                        .Replace(" ", "");
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Cannot find TestContext");
            }
        }
    }
}
