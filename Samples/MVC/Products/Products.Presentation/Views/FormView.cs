using System.Collections.Generic;
using System.Web.Mvc;
using MvcContrib.FluentHtml;
using MvcContrib.FluentHtml.Behaviors;

namespace Products.Presentation.Views
{
    public class FormView<T> : ViewPage<T>, IViewModelContainer<T> where T : class
    {
        private readonly IEnumerable<IBehaviorMarker> _behaviors;

        public FormView()
        {
            _behaviors = new[]
                             {
                                 new ValidationBehavior(() => ViewData.ModelState)
                             };
        }

        public T ViewModel
        {
            get { return Model; }
        }

        public IEnumerable<IBehaviorMarker> Behaviors
        {
            get { return _behaviors; }
        }

        public string HtmlNamePrefix { get; set; }
    }
}