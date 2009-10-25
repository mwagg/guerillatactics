using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuerillaTactics.Common.Utility
{
    public class ObjectFieldMaper
    {
        public void Map(object source, object target)
        {
            IEnumerable<FieldInfo> sourceFields = GetFieldsForType(source.GetType());
            foreach (var sourceFieldInfo in sourceFields)
            {
                object sourceValue = sourceFieldInfo.GetValue(source);
                var targetFieldInfo = target.GetType().GetField(sourceFieldInfo.Name,
                                                                BindingFlags.Instance | BindingFlags.NonPublic);

                if (targetFieldInfo != null)
                {
                    object targetValue = null;

                    if (sourceFieldInfo.FieldType == targetFieldInfo.FieldType)
                    {
                        targetValue = sourceValue;
                    }
                    if(targetFieldInfo.FieldType == typeof(string))
                    {
                        targetValue = sourceValue.ToString();
                    }
                    if(sourceFieldInfo.FieldType == typeof(string))
                    {
                        targetValue = Convert.ChangeType(sourceValue, targetFieldInfo.FieldType);
                    }
                
                    targetFieldInfo.SetValue(target, targetValue);
                }
            }
        }

        private IEnumerable<FieldInfo> GetFieldsForType(Type type)
        {
            if(type == null)
            {
                return new FieldInfo[]{};
            }

            return type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Union(GetFieldsForType(type.BaseType));
        }
    }
}