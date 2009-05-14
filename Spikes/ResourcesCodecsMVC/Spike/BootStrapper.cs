using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Core.Code.ResponseCodecs;
using Microsoft.Practices.ServiceLocation;

namespace Spike
{
    public class BootStrapper
    {
        public void Configure()
        {
            RegisterRoutes(RouteTable.Routes);
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            var container = new WindsorContainer();

            container.Register(AllTypes.FromAssembly(typeof (IResponseCodec).Assembly)
                                   .BasedOn(typeof (IResponseCodec))
                                   .Configure(config => config.LifeStyle.Transient));

            ServiceLocator.SetLocatorProvider(()=> new WindsorServiceLocator(container));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );

        }
    }
}