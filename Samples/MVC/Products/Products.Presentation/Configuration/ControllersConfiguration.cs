using System.Web.Mvc;
using Castle.Windsor;
using MvcContrib.Castle;

namespace Products.Presentation.Configuration
{
    public class ControllersConfiguration : IApplicationConfiguration
    {
        public void Execute(IWindsorContainer container)
        {
            container.RegisterControllers(GetType().Assembly);

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}