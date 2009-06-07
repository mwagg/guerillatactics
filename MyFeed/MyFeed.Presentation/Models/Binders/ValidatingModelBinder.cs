using System.Web.Mvc;
using Castle.Components.Validator;

namespace MyFeed.Presentation.Models.Binders
{
    public abstract class ValidatingModelBinder<TModel> : ModelBinderBase<TModel>
    {
        private readonly IValidatorRegistry _validatorRegistry;

        protected ValidatingModelBinder(IValidatorRegistry validatorRegistry)
        {
            _validatorRegistry = validatorRegistry;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            TModel model = GetModelFromBindingContext(bindingContext);

            var validationRunner = new ValidatorRunner(_validatorRegistry);
            if (validationRunner.IsValid(model) == false)
            {
                AddErrorsToModelState(bindingContext, validationRunner, model);
            }

            return model;
        }

        private void AddErrorsToModelState(ModelBindingContext bindingContext, ValidatorRunner validationRunner, 
                                           TModel model)
        {
            ErrorSummary errorSummary = validationRunner.GetErrorSummary(model);

            foreach (var invalidProperty in errorSummary.InvalidProperties)
            {
                var modelState = new ModelState();

                foreach (var errorForProperty in errorSummary.GetErrorsForProperty(invalidProperty))
                {
                    modelState.Errors.Add(errorForProperty);
                }

                bindingContext.ModelState.Add(invalidProperty, modelState);
            }
        }

        protected abstract TModel GetModelFromBindingContext(ModelBindingContext bindingContext);
    }
}