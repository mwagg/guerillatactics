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

        protected override void OnModelUpdated(ControllerContext controllerContext, 
            ModelBindingContext bindingContext)
        {
            base.OnModelUpdated(controllerContext, bindingContext);

            var validationRunner = new ValidatorRunner(_validatorRegistry);
            if (validationRunner.IsValid(bindingContext.Model) == false)
            {
                AddErrorsToModelState(bindingContext, validationRunner, 
                    bindingContext.Model);
            }
        }

        private void AddErrorsToModelState(ModelBindingContext bindingContext, 
            IValidatorRunner validationRunner, 
                                           object model)
        {
            ErrorSummary errorSummary = validationRunner.GetErrorSummary(model);

            foreach (var invalidProperty in errorSummary.InvalidProperties)
            {
                foreach (var errorForProperty in 
                    errorSummary.GetErrorsForProperty(invalidProperty))
                {
                    bindingContext.ModelState.AddModelError(invalidProperty, errorForProperty);
                }
            }
        }
    }
}