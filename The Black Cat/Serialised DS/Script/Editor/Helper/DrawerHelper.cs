using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    internal static class DrawerHelper
    {
        public static readonly List<int> EmptyList = new List<int>();

        public static float CalculateLabelWidth(string label)
        {
            return EditorStyles.label.CalcSize(new GUIContent(label)).x + 5;
        }

        public static bool HasCustomDrawer(SerializedProperty property)
        {
            var type = typeof(SerializedProperty).Assembly.GetType("UnityEditor.ScriptAttributeUtility");
            if (type != null)
            {
                var method = type.GetMethod("GetDrawerTypeForPropertyAndType", BindingFlags.NonPublic | BindingFlags.Static);
                return method?.Invoke(null, new object[] { property, type }) != null;
            }
            return false;
        }

        public static string GetPropertyKey(SerializedProperty property)
        {
            return property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
        }

        public static object GetPropertyTargetObject(SerializedProperty prop, object target)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    target = GetValue(target, elementName, index);
                }
                else
                {
                    target = GetValue(target, element);
                }
            }
            return target;
        }

        public static object GetValue(object source, string name)
        {
            if (source == null)
                return null;
            var type = source.GetType();
            var f = type.GetFieldRecursive(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (f == null)
            {
                var p = type.GetPropertyRecursive(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (p == null)
                    return null;
                return p.GetValue(source, null);
            }
            return f.GetValue(source);
        }

        public static object GetValue(object source, string name, int index)
        {
            var enumerable = GetValue(source, name) as IEnumerable;
            var enm = enumerable.GetEnumerator();
            while (index-- >= 0)
                enm.MoveNext();
            return enm.Current;
        }

        private static FieldInfo GetFieldRecursive(this Type type, string name, BindingFlags bindingFlags)
        {
            var fieldInfo = type.GetField(name, bindingFlags);
            if (fieldInfo == null && type.BaseType != null)
                return type.BaseType.GetFieldRecursive(name, bindingFlags);
            return fieldInfo;
        }

        private static PropertyInfo GetPropertyRecursive(this Type type, string name, BindingFlags bindingFlags)
        {
            var propertyInfo = type.GetProperty(name, bindingFlags);
            if (propertyInfo == null && type.BaseType != null)
                return type.BaseType.GetPropertyRecursive(name, bindingFlags);
            return propertyInfo;
        }
    }
}
