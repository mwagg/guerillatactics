using System.Web.Mvc;
using Castle.Components.Validator;
using GuerillaTactics.Common.Utility;

namespace MyFeed.Presentation.Models.Binders
{
    public class PostNewFeedUpdateModelBinder : ValidatingModelBinder<PostNewFeedUpdateModel>
    {
        public PostNewFeedUpdateModelBinder(IValidatorRegistry validatorRegistry)
            : base(validatorRegistry)
        {
        }

        protected override PostNewFeedUpdateModel GetModelFromBindingContext(ModelBindingContext bindingContext)
        {
            return new PostNewFeedUpdateModel
                       {
                           Username = GetAttemptedValue(bindingContext, m => m.Username),
                           Content = GetAttemptedValue(bindingContext, m => m.Content),
                           PublishedDate = SystemTime.Now()
                       };
        }
    }
}