using FluentFramework.Core;
using NUnit.Framework;

namespace FluentFramework.NUnit
{
    [TestFixture]
    public class NUnitTestBase : TestBase
    {
        [SetUp]
        public void Setup()
        {
            base.TestSetup();
        }

        [TearDown]
        public void TearDown()
        {
            base.Cleanup();
        }
    }
}
