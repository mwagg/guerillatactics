using System.Web.Mvc;
using System.Web.Routing;

namespace MyFeed.Presentation.Configuration
{
    public class MyFeedRoutingConfiguration
    {
        public static readonly string FeedUpdateRoute = "FeedUpdate";
        public static string ViewFeedRoute = "ViewFeed";

        public void Configure(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(FeedUpdateRoute, "feed/post",
                            new
                                {
                                    controller = "FeedUpdate",
                                    action = "Index"
                                });
            routes.MapRoute(ViewFeedRoute, "feed/{username}",
                            new
                                {
                                    controller = "Feed",
                                    action = "Index"
                                });
        }
    }
}