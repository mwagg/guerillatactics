using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyFeed.Core.Infrastructure
{
    public static class HtmlExtensions
    {
        public static ForEachHelper<T> ForEach<T>(this HtmlHelper html, IEnumerable<T> items)
        {
            return new ForEachHelper<T>(html, items);
        }
    }
    
    public class ForEachHelper<T>
    {
        private readonly HtmlHelper _html;
        private readonly IEnumerable<T> _items;

        public ForEachHelper(HtmlHelper html, IEnumerable<T> items)
        {
            _html = html;
            _items = items;
        }

        public void Li(Action<T> action)
        {
            foreach (var item in _items)
            {
                _html.ViewContext.HttpContext.Response.Write("<li>");
                action(item);
                _html.ViewContext.HttpContext.Response.Write("</li>");
            }
        }
    }
}