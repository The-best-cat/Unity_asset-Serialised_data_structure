using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    [CustomPropertyDrawer(typeof(IndividualIdentity))]
    public class PersonIdDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var prop_fname = property.FindPropertyRelative("firstName");
            var prop_lname = property.FindPropertyRelative("lastName");
            Rect foldout = position.WithHeight(EditorGUIUtility.singleLineHeight);

            string fullName = string.IsNullOrEmpty(prop_fname.stringValue.Trim()) && string.IsNullOrEmpty(prop_lname.stringValue.Trim()) ? "" : prop_fname.stringValue.Trim() + " " + prop_lname.stringValue.Trim();
            property.isExpanded = EditorGUI.Foldout(foldout, property.isExpanded, string.IsNullOrEmpty(fullName) ? "Identity" : fullName);
            if (property.isExpanded)
            {
                Rect label_fname = foldout.MoveX(11).AppendBottom(2).WithWidth(80);
                Rect label_lname = label_fname.AppendBottom(2).WithWidth(80);
                Rect label_id = label_lname.AppendBottom(2).WithWidth(80);

                Rect r_fname = label_fname.AppendRight().WithWidth(foldout.width - 91);
                Rect r_lname = label_lname.AppendRight().WithWidth(foldout.width - 91);
                Rect r_id = label_id.AppendRight().WithWidth(foldout.width - 91);

                EditorGUI.LabelField(label_fname, "First Name");
                EditorGUI.LabelField(label_lname, "Last Name");
                EditorGUI.LabelField(label_id, "ID");

                EditorGUI.PropertyField(r_fname, prop_fname, GUIContent.none);
                EditorGUI.PropertyField(r_lname, prop_lname, GUIContent.none);
                EditorGUI.PropertyField(r_id, property.FindPropertyRelative("id"), GUIContent.none);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return property.isExpanded ? EditorGUIUtility.singleLineHeight * 4 + 8 : EditorGUIUtility.singleLineHeight;
        }
    }
}