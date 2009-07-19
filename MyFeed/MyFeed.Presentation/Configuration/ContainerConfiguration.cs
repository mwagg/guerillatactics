using Castle.Windsor;
using MvcContrib.Castle;

namespace MyFeed.Presentation.Configuration
{
    public class ContainerConfiguration
    {
        public void Configure(IWindsorContainer container)
        {
            container.RegisterWithSelf();
            container.RegisterControllers(BootStrapper.PresentationAssembly);
            container.RegisterValidationRegistry();
            container.RegisterComponentsFromCore();
        }
    }
}