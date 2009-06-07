using System.Web.Mvc;
using Castle.Components.Validator;
using MyFeed.Presentation.Models.Login;

namespace MyFeed.Presentation.Models.Binders
{
    public class UserCredentialsModelBinder : ValidatingModelBinder<UserCredentials>
    {
        public UserCredentialsModelBinder(IValidatorRegistry validatorRegistry) : base(validatorRegistry)
        {
        }

        protected override UserCredentials GetModelFromBindingContext(ModelBindingContext bindingContext)
        {
            return new UserCredentials
            {
                Username = bindingContext.ValueProvider["Username"].AttemptedValue,
                Password = bindingContext.ValueProvider["Password"].AttemptedValue
            };
        }
    }
}