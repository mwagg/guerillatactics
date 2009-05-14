using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}