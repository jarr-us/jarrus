using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jarrus.GA.Utility
{
    public static class Reflection
    {
        public static object GetObjectFromType(string fullyQualifiedName)
        {
            var elementType = Type.GetType(fullyQualifiedName);
            if (elementType == null) return null;
            return (Activator.CreateInstance(elementType));
        }

        public static void CopyProperties(this object source, object destination)
        {
            if (source == null || destination == null) { throw new Exception("Source and destination objects can not be be null"); }
                
            var typeDest = destination.GetType();
            var typeSrc = source.GetType();

            var srcProps = typeSrc.GetProperties();
            foreach (var srcProp in srcProps)
            {
                if (!srcProp.CanRead) { continue; }
                var targetProperty = typeDest.GetProperty(srcProp.Name);

                if (targetProperty == null) { continue; }
                if (!targetProperty.CanWrite) { continue; }
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate) { continue; }
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0) { continue; }
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)) { continue; }
                
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }

            var sourceFields = typeSrc.GetFields();
            var destinationFields = new List<FieldInfo>(typeDest.GetFields());
            foreach (var fieldInfo in sourceFields)
            {
                var str = fieldInfo.ToString();                
                foreach(var destField in typeDest.GetFields())
                {
                    if (destField.ToString().Equals(str))
                    {
                        fieldInfo.SetValue(destination, fieldInfo.GetValue(source));
                    }                    
                }
            }
        }
    }
}