using System.Web.Mvc;
using GuerillaTactics.Testing;
using GuerillaTactics.Testing.Mvc;
using MyFeed.Presentation.Controllers;
using NUnit.Framework;

namespace specs_for_HomeController
{
    public abstract class base_context : Specification<HomeController>
    {
        protected override HomeController CreateSubject()
        {
            return new HomeController();
        }
    }

    [TestFixture]
    public class when_requesting_the_Index_action : base_context
    {
        private ActionResult result;

        protected override void When()
        {
            result = Subject.Index();
        }

        [Test]
        public void the_index_view_should_be_rendered()
        {
            result.should_render_default_view_for_action();
        }
    }
}