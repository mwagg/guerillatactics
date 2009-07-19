using Castle.Components.Validator;

namespace MyFeed.Presentation.Models.Binders
{
    public class FeedUpdateViewModelBinder : ValidatingModelBinder<FeedUpdateViewModel>
    {
        public FeedUpdateViewModelBinder(IValidatorRegistry validatorRegistry) 
            : base(validatorRegistry)
        {
        }
    }
}