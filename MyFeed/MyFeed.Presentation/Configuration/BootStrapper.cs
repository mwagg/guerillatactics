using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;

namespace MyFeed.Presentation.Configuration
{
    public class BootStrapper
    {
        public static Assembly PresentationAssembly = typeof (BootStrapper).Assembly;
        public static Assembly CoreAssembly = Assembly.Load("MyFeed.Core");

        public void Go()
        {
            var container = new WindsorContainer();

            new MyFeedRoutingConfiguration().Configure(RouteTable.Routes);
            new ContainerConfiguration().Configure(container);
            new ModelBinderConfiguration().Configure(container);
            
            ConfigureServiceLocator(container);
        }

        private void ConfigureServiceLocator(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}