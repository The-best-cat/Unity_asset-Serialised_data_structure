using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class QueueRL : RLDrawer
    {
        public QueueRL(SerializedProperty property, GUIContent label, FieldInfo info) : base(property, label, info)
        {
            reorderableList.drawFooterCallback += OnDrawFooter;
        }

        protected override void DrawRemoveButton(Rect rect, int index)
        {
            if ((Application.isPlaying && index == 0) || !Application.isPlaying)
            {
                base.DrawRemoveButton(rect, index);
            }            
        }

        private void OnDrawFooter(Rect rect)
        {
            if (GUI.Button(rect.AppendRight().MoveX(-71).MoveY(2).WithWidth(65).WithHeight(EditorGUIUtility.singleLineHeight), "¡Ï"))
            {
                listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
                listProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected override string NoneElementText(string name)
        {
            return base.NoneElementText("Queue");
        }
    }
}