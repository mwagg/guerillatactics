using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Components.Validator;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using GuerillaTactics.Common.Utility;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using MyFeed.Core.Domain.Model;
using MyFeed.Core.Domain.Queries;
using MyFeed.Core.Domain.Services;
using MyFeed.Core.Infrastructure.MVC.Codecs;
using MyFeed.Presentation.Models.Binders;

namespace MyFeed.Presentation
{
    public class BootStrapper
    {
        public static Assembly PresentationAssembly = typeof (BootStrapper).Assembly;
        public static Assembly CoreAssembly = typeof (FeedUpdate).Assembly;

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                Routes.HomePage,
                "",
                new {controller = "Home", action = "Index"});
            routes.MapRoute(
                Routes.Logout,
                "logout/",
                new {controller = "Login", action = "Logout"});
            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );
        }

        public void Go()
        {
            RegisterRoutes(RouteTable.Routes);
            var container = new WindsorContainer();
            ConfigureContainer(container);
            ConfigureModelBinders(container);
            ConfigureServiceLocator(container);
        }

        private void ConfigureServiceLocator(IWindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }

        private void ConfigureModelBinders(IWindsorContainer container)
        {
            foreach (var modelBinderType in PresentationAssembly.GetExportedTypes()
                .Where(t => t.IsAbstract == false && 
                    t.IsSubClassOfRawGenericType(typeof(ModelBinderBase<>))))
            {
                Type modelType = modelBinderType.GetGenericArgumentsFromBase().Single();
                ModelBinders.Binders.Add(modelType, (IModelBinder)container.Resolve(modelBinderType));
            }
        }

        private void ConfigureContainer(IWindsorContainer container)
        {
            container.RegisterWithSelf();
            container.RegisterControllers(PresentationAssembly);
            container.RegisterQueries();
            container.RegisterResponseCodecs();
            container.RegisterModelBinders();
            container.RegisterValidationRegistry();
            container.RegisterDomainServices();
            container.RegisterQueries();
        }
    }

    public static class ContainerConfigurationExtensions
    {
        public static void RegisterQueries(this IWindsorContainer container)
        {
            container.Register(AllTypes
                                   .FromAssembly(BootStrapper.CoreAssembly)
                                   .Where(t => t.Namespace == typeof (FindUserByUsernameQuery).Namespace)
                                   .WithService.FirstInterface()
                                   .Configure(config => config.LifeStyle.Transient));
        }

        public static void RegisterDomainServices(this IWindsorContainer container)
        {
            container.Register(AllTypes
                .FromAssembly(BootStrapper.CoreAssembly)
                .Where(t => t.Namespace ==
                            typeof(IUserAuthenticationService).Namespace)
                .WithService.FirstInterface()
                .Configure(config => config.LifeStyle.Transient));
        }

        public static void RegisterValidationRegistry(this IWindsorContainer container)
        {
            container.Register(Component
                .For<IValidatorRegistry>()
                .Instance(new CachedValidationRegistry()));
        }

        public static void RegisterResponseCodecs(this IWindsorContainer container)
        {
            container.Register(AllTypes
                .Of<IResponseCodec>()
                .FromAssembly(BootStrapper.CoreAssembly)
                .Configure(config => config.LifeStyle.Transient));
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
    }
}