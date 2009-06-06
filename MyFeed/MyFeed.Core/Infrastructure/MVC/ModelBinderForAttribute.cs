using System;

namespace MyFeed.Core.Infrastructure.MVC
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelBinderForAttribute : Attribute
    {
        private readonly Type _modelType;

        public ModelBinderForAttribute(Type modelType)
        {
            _modelType = modelType;
        }

        public Type ModelType
        {
            get { return _modelType; }
        }
    }
}