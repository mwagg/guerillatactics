using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using GuerillaTactics.Common.Utility;

namespace MyFeed.Presentation.Models.Binders
{
    public abstract class ModelBinderBase<TModel> : IModelBinder
    {
        public abstract object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);

        public string GetAttemptedValue(ModelBindingContext bindingContext,
                                        Expression<Func<TModel, object>> propertyExpression)
        {
            string key = ReflectionExtensions.GetPropertyName(propertyExpression);
            return bindingContext.ValueProvider[key].AttemptedValue;
        }
    }
}