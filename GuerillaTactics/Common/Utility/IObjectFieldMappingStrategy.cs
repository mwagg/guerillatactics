using System;

namespace GuerillaTactics.Common.Utility
{
    public interface IObjectFieldMappingStrategy
    {
        bool ShouldMapField(System.Reflection.FieldInfo fieldInfo);
    }
}