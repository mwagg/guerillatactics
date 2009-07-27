using System;
using System.Collections.Generic;

namespace Products.Presentation.Views
{
    public static class ViewHelpers
    {
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
    }
}