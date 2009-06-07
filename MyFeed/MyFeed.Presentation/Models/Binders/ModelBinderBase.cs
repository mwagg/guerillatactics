using System.Web.Mvc;

namespace MyFeed.Presentation.Models.Binders
{
    public abstract class ModelBinderBase<TModel> : IModelBinder
    {
        public abstract object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
    }
}