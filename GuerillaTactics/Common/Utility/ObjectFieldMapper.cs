#region licence

// * Copyright (c) 2009, Michael Wagg
// * All rights reserved.
// *
// * Redistribution and use in source and binary forms, with or without
// * modification, are permitted provided that the following conditions are met:
// *     * Redistributions of source code must retain the above copyright
// *       notice, this list of conditions and the following disclaimer.
// *     * Redistributions in binary form must reproduce the above copyright
// *       notice, this list of conditions and the following disclaimer in the
// *       documentation and/or other materials provided with the distribution.
// *     * Neither the name Michael Wagg nor the
// *       names of its contributors may be used to endorse or promote products
// *       derived from this software without specific prior written permission.
// *
// * THIS SOFTWARE IS PROVIDED BY Michael Wagg ''AS IS'' AND ANY
// * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// * DISCLAIMED. IN NO EVENT SHALL Michael Wagg BE LIABLE FOR ANY
// * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuerillaTactics.Common.Utility
{
    public class ObjectFieldMapper
    {
        private readonly IObjectFieldMappingStrategy _mappingStrategy;

        public ObjectFieldMapper(IObjectFieldMappingStrategy mappingStrategy)
        {
            _mappingStrategy = mappingStrategy;
        }

        public ObjectFieldMapper() : this(new DefaultObjectFieldMappingStrategy())
        {
        }

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
                    if (targetFieldInfo.FieldType == typeof (string))
                    {
                        targetValue = sourceValue.ToString();
                    }
                    if (sourceFieldInfo.FieldType == typeof (string))
                    {
                        targetValue = Convert.ChangeType(sourceValue, targetFieldInfo.FieldType);
                    }

                    targetFieldInfo.SetValue(target, targetValue);
                }
            }
        }

        private IEnumerable<FieldInfo> GetFieldsForType(Type type)
        {
            if (type == null)
            {
                return new FieldInfo[] {};
            }

            var allFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            return allFields.Where(field => _mappingStrategy.ShouldMapField(field))
                .Union(GetFieldsForType(type.BaseType));
        }
    }
}