using System.Web.Mvc;
using Castle.Components.Validator;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace MyFeed.Presentation.Configuration
{
    public static class ContainerConfigurationExtensions
    {
        public static void RegisterValidationRegistry(this IWindsorContainer container)
        {
            container.Register(Component
                                   .For<IValidatorRegistry>()
                                   .Instance(new CachedValidationRegistry()));
        }

        public static void RegisterModelBinders(this IWindsorContainer container)
        {
            container.Register(AllTypes
                                   .Of<IModelBinder>()
                                   .FromAssembly(BootStrapper.PresentationAssembly));
        }

        public static void RegisterWithSelf(this IWindsorContainer container)
        {
            container.Register(Component.For<IWindsorContainer>()
                                   .Instance(container));
        }

        public static void RegisterComponentsFromCore(this IWindsorContainer container)
        {
            container.Register(AllTypes.FromAssembly(BootStrapper.CoreAssembly)
                                   .Where(t => t.GetInterfaces().Length > 0)
                                   .WithService.FirstInterface()
                                   .Configure(config => config.LifeStyle.Transient));
        }
    }
}