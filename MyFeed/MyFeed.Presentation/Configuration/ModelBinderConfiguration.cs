using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Windsor;
using GuerillaTactics.Common.Utility;
using MyFeed.Presentation.Models.Binders;

namespace MyFeed.Presentation.Configuration
{
    public class ModelBinderConfiguration
    {
        public void Configure(IWindsorContainer container)
        {
            container.RegisterModelBinders();

            foreach (var modelBinderType in BootStrapper.PresentationAssembly.GetExportedTypes()
                .Where(t => t.IsAbstract == false &&
                    t.IsSubClassOfRawGenericType(typeof(ModelBinderBase<>))))
            {
                Type modelType = modelBinderType.GetGenericArgumentsFromBase().Single();
                ModelBinders.Binders.Add(modelType, (IModelBinder)container.Resolve(modelBinderType));
            }
        }
    }
}