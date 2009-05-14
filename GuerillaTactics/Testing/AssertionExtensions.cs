using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace GuerillaTactics.Testing
{
	public static class AssertionExtensions
	{
		public static void should_be_true(this bool condition)
		{
			Assert.That(condition, Is.True);
		}

		public static void should_be_false(this bool condition)
		{
			Assert.That(condition, Is.False);
		}

		public static void should_be_equal_to(this object actual, object expected)
		{
			Assert.That(actual, Is.EqualTo(expected));
		}

		public static void should_be_equivalent_to(this IEnumerable actual, ICollection expected)
		{
			Assert.That(actual, Is.EquivalentTo(expected));
		}

		public static void should_not_be_equals_to(this object actual, object expected)
		{
			Assert.That(actual, Is.Not.EqualTo(expected));
		}

		public static void should_be_the_same_as(this object actual, object expected)
		{
			Assert.That(actual, Is.SameAs(expected));
		}

		public static void should_not_be_the_same_as(this object actual, object expected)
		{
			Assert.That(actual, Is.Not.SameAs(expected));
		}

		public static void should_contain(this ICollection actual, object expected)
		{
			Assert.Contains(expected, actual);
		}

		public static void should_be_greater_than(this IComparable arg1, IComparable arg2)
		{
			Assert.That(arg1, Is.GreaterThan(arg2));
		}

		public static void should_be_greater_than_or_equal_to(this IComparable arg1, IComparable arg2)
		{
			Assert.That(arg1, Is.GreaterThanOrEqualTo(arg2));
		}

		public static void should_be_less_than(this IComparable arg1, IComparable arg2)
		{
			Assert.That(arg1, Is.LessThan(arg2));
		}

		public static void should_be_less_than_or_equal_to(this IComparable arg1, IComparable arg2)
		{
			Assert.That(arg1, Is.LessThanOrEqualTo(arg2));
		}

		public static void should_be_assignable_from(this object actual, Type expected)
		{
			Assert.That(actual, Is.AssignableFrom(expected));
		}

		public static void should_not_be_assignable_from(this object actual, Type expected)
		{
			Assert.That(actual, Is.Not.AssignableFrom(expected));
		}

		public static void should_be_empty(this string value)
		{
			Assert.That(value, Is.Empty);
		}

		public static void should_not_be_empty(this string value)
		{
			Assert.That(value, Is.Not.Empty);
		}

		public static void should_be_empty(this ICollection collection)
		{
			Assert.That(collection, Is.Empty);
		}

		public static void should_be_empty<T>(this ICollection<T> collection)
		{
			Assert.That(collection.Count, Is.EqualTo(0), "Expected collection to be empty but was not.");
		}

		public static void should_not_be_empty(this ICollection collection)
		{
			Assert.That(collection, Is.Not.Empty);
		}

        public static void should_be_instance_of_type<T>(this object actual)
        {
            actual.should_be_instance_of_type(typeof(T));
        }

		public static void should_be_instance_of_type(this object actual, Type expected)
		{
			Assert.That(actual, Is.InstanceOf(expected));
		}

		public static void should_not_be_instance_of_type(this object actual, Type expected)
		{
			Assert.That(actual, Is.Not.InstanceOf(expected));
		}

		public static void should_not_be_instance_of_type<TException>(this object actual)
		{
			actual.should_not_be_instance_of_type(typeof(TException));
		}

		public static void should_be_NaN(this double value)
		{
			Assert.That(value, Is.NaN);
		}

		public static void should_be_null(this object value)
		{
			Assert.That(value, Is.Null);
		}

		public static void should_not_be_null(this object value)
		{
			Assert.That(value, Is.Not.Null);
		}

		public static void should_be_thrown_by(this Type exceptionType, Action action)
		{
			Exception e = null;

			try
			{
				action();
			}
			catch (Exception ex)
			{
				e = ex;
			}

			e.should_not_be_null();
			e.should_be_instance_of_type(exceptionType);
		}

		public static void should_not_be_empty(this IEnumerable actual)
		{
			Assert.That(actual, Is.Not.Empty);
		}
	}
}