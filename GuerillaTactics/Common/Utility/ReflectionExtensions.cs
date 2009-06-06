using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GuerillaTactics.Common.Utility
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetAllExportedTypesAssignableTo<T>(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .Where(t => typeof (T).IsAssignableFrom(t));
        }

        public static string GetPropertyName<T>(this T source, Expression<Func<T, object>> propertyAccessor)
        {
            return ((MemberExpression)propertyAccessor.Body).Member.Name;
        }
    }
}