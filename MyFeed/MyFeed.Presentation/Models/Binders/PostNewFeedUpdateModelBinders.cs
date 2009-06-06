using System.Web.Mvc;
using Castle.Components.Validator;
using GuerillaTactics.Common.Utility;
using MyFeed.Core.Infrastructure.MVC;

namespace MyFeed.Presentation.Models.Binders
{
    [ModelBinderFor(typeof(PostNewFeedUpdateModel))]
    public class PostNewFeedUpdateModelBinders : IModelBinder
    {
        private IValidatorRegistry _validatorRegistry;

        public PostNewFeedUpdateModelBinders(IValidatorRegistry validatorRegistry)
        {
            _validatorRegistry = validatorRegistry;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var validationRunner = new ValidatorRunner(_validatorRegistry);

            var model = new PostNewFeedUpdateModel
                            {
                                Username = bindingContext.ValueProvider["username"].AttemptedValue,
                                Content = bindingContext.ValueProvider["content"].AttemptedValue,
                                PublishedDate = SystemTime.Now()
                            };

            if (validationRunner.IsValid(model) == false)
            {
                ErrorSummary errorSummary = validationRunner.GetErrorSummary(model);
                
                foreach (var invalidProperty in errorSummary.InvalidProperties)
                {
                    var modelState = new ModelState();

                    foreach (var errorForProperty in  errorSummary.GetErrorsForProperty(invalidProperty))
                    {
                        modelState.Errors.Add(errorForProperty);
                    }

                    bindingContext.ModelState.Add(invalidProperty, modelState);
                }
            }

            return model;
        }
    }
}