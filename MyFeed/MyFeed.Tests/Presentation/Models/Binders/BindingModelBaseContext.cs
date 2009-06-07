using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using Castle.Components.Validator;
using GuerillaTactics.Common.Utility;
using GuerillaTactics.Testing;

namespace MyFeed.Tests.Presentation.Models.Binders
{
    public abstract class BindingModelBaseContext<TBinder, TModel> :
        Specification<TBinder>
        where TBinder : IModelBinder
        where TModel : class
    {
        private ModelBindingContext binding_context;
        protected ControllerContext controller_context;
        private object model;
        protected IValidatorRegistry validationRegistry = new CachedValidationRegistry();

        protected TModel TypedModel
        {
            get { return model as TModel; }
        }

        protected override void When()
        {
            model = Subject.BindModel(controller_context, binding_context);
        }

        protected void AddRequestValue(string valueKey, string value)
        {
            binding_context.ValueProvider.Add(valueKey,
                                              new ValueProviderResult(value, value, CultureInfo.CurrentCulture));
        }

        protected void AddRequestValue(Expression<Func<TModel, object>> propertyExpression, string value)
        {
            AddRequestValue(ReflectionExtensions.GetPropertyName(propertyExpression), value);
        }

        protected override void EstablishContext()
        {
            base.EstablishContext();

            binding_context = new ModelBindingContext();
            binding_context.ValueProvider = new Dictionary<string, ValueProviderResult>();
        }

        protected ModelStateDictionary ModelState
        {
            get { return binding_context.ModelState; }
        }
    }
}