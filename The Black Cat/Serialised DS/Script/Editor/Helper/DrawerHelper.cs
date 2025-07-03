using System.Collections.Generic;
using System.Reflection;
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
    }
}
