using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace Products.Presentation.Configuration
{
    public class RoutingConfiguration : IApplicationConfiguration
    {
        public void Execute(IWindsorContainer container)
        {
            var routes = RouteTable.Routes;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                RouteNames.ProductListing,
                "products/",
                new {controller = "ProductListing", action = "Index"});
            routes.MapRoute(
                RouteNames.AddNewProduct,
                "products/add/",
                new {controller = "AddNewProduct", action = "Index"});
        }
    }
}