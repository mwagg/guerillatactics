using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Products.Presentation.Configuration
{
    public static class WindsorConfigurationExtensions
    {
        public static void RegisterWithSelf(this IWindsorContainer container)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container));
        }
    }
}