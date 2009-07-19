using System.Web.Mvc;
using MyFeed.Core.Domain.Services;
using MyFeed.Presentation.Configuration;
using MyFeed.Presentation.Models;

namespace MyFeed.Presentation.Controllers
{
    public class FeedUpdateController : Controller
    {
        private readonly ICurrentUserDetailsProvider _currentUserDetailsProvider;

        public FeedUpdateController(ICurrentUserDetailsProvider currentUserDetailsProvider)
        {
            _currentUserDetailsProvider = currentUserDetailsProvider;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FeedUpdateViewModel feedUpdate)
        {
            if(ModelState.IsValid)
            {
                return RedirectToRoute(MyFeedRoutingConfiguration.ViewFeedRoute,
                                       new {username = _currentUserDetailsProvider.GetUsername()});
            }
            return View(feedUpdate);
        }
    }
}