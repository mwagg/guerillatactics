using System;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;
using NUnit.Framework;

namespace UnitTests.Utility
{
    [TestFixture]
    public class ReflectionExtensionsTests
    {
        public string AProperty { get { return String.Empty; } }

        [Test]
        public void GetPropertyName_returns_the_correct_name()
        {
            this.GetPropertyName(t => t.AProperty).should_be_equal_to("AProperty");
        }
    }
}