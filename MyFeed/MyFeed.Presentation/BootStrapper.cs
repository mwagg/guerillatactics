using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommonServiceLocator.WindsorAdapter;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Castle;
using MyFeed.Core.Domain.Model;
using MyFeed.Core.Infrastructure.MVC;
using MyFeed.Core.Infrastructure.MVC.Codecs;

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
                RouteNames.ViewUsersFeed,
                "{username}/",
                new {controller = "Feed", action = "Index"}
                );
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
                .Where(t => typeof(IModelBinder).IsAssignableFrom(t)))
            {
                var modelTypeAttributes = modelBinderType.GetCustomAttributes(typeof (ModelBinderForAttribute), true);

                if(modelTypeAttributes.Length == 0)
                {
                    throw new ConfigurationErrorsException(String.Format("Model binder type {0} is not decorated with {1}. The convention is to use this attribute on all model binders.",
                        modelBinderType.Name, typeof(ModelBinderForAttribute).Name));
                }

                foreach (ModelBinderForAttribute modelTypeAttribute in modelTypeAttributes)
                {
                    ModelBinders.Binders.Add(modelTypeAttribute.ModelType, (IModelBinder)container.Resolve(modelBinderType));
                }
            }
        }

        private void ConfigureContainer(IWindsorContainer container)
        {
            container.RegisterWithSelf();
            container.RegisterControllers(GetType().Assembly);
            container.RegisterQueries();
            container.RegisterResponseCodecs();
            container.RegisterModelBinders();
        }
    }

    public static class ContainerConfigurationExtensions
    {
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

        public static void RegisterQueries(this IWindsorContainer container)
        {
            container.Register(AllTypes.FromAssembly(BootStrapper.CoreAssembly)
                                   .Where(type => type.Namespace == "MyFeed.Core.Domain.Queries")
                                   .WithService.FirstInterface()
                                   .Configure(config => config.LifeStyle.Transient));
        }
    }
}