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

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyAccessor)
        {
            return ((MemberExpression)propertyAccessor.Body).Member.Name;
        }

        public static bool IsSubClassOfRawGenericType(this Type toCheck, Type generic)
        {
            while (toCheck != typeof(object))
            {
                var current = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == current)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static Type[] GetGenericArgumentsFromBase(this Type originalType)
        {
            var currentType = originalType;
            while(originalType != typeof(object))
            {
                if(currentType.IsGenericType)
                {
                    return currentType.GetGenericArguments();
                }

                currentType = originalType.BaseType;
            }

            return new Type[]{};
        }
    }
}