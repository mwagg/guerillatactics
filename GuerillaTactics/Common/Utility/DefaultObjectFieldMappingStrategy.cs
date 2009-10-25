using System.Reflection;

namespace GuerillaTactics.Common.Utility
{
    public class DefaultObjectFieldMappingStrategy : IObjectFieldMappingStrategy
    {
        public virtual bool ShouldMapField(FieldInfo fieldInfo)
        {
            return true;
        }
    }
}