using System;
using System.Linq;
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
        public void GetPropertyName_returns_the_correct_name_from_instance()
        {
            this.GetPropertyName(t => t.AProperty).should_be_equal_to("AProperty");
        }

        [Test]
        public void GetPropertyName_returns_the_correct_name_from_type()
        {
            ReflectionExtensions.GetPropertyName<ReflectionExtensionsTests>(
                t => t.AProperty).should_be_equal_to("AProperty");
        }

        [Test]
        public void IsSubClassOfRawGeneric_correctly_identifies_a_subclass_of_a_raw_generic_type()
        {
            typeof (FooBar).IsSubClassOfRawGenericType(typeof (Foo<>)).should_be_true();
        }

        [Test]
        public void GetGenericArgumentsFromBase_gets_correct_parameter_type()
        {
            typeof(FooBar).GetGenericArgumentsFromBase().Single().should_be_equal_to(typeof(AClass));
        }
    }

    public class AClass{}
    public class Foo<T>{}
    public class FooBar : Foo<AClass>{}
}