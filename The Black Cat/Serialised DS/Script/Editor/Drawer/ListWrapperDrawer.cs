using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    [CustomPropertyDrawer(typeof(ListWrapper<>), true)]
    public class ListWrapperDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, property.FindPropertyRelative("List"), label);

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("List"));
        }
    }
}