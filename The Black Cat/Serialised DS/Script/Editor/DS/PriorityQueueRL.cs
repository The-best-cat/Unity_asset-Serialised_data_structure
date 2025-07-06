using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class PriorityQueueRL : RLDrawer
    {
        private GUIStyle style;

        public PriorityQueueRL(SerializedProperty property, GUIContent label, FieldInfo info) : base(property, label, info)
        {
            reorderableList.draggable = false;
            style = new GUIStyle(EditorStyles.miniLabel)
            {
                alignment = TextAnchor.MiddleRight
            };
        }

        public override float GetPropertyHeight()
        {
            if (listProperty == null)
            {
                return 0;
            }
            if (listProperty.isExpanded)
            {
                return reorderableList.GetHeight() - EditorGUIUtility.singleLineHeight;
            }
            return 20;
        }

        protected override void DrawElementCount(Rect rect)
        {
            string countText = listProperty.arraySize == 1 ? "1 element" : $"{listProperty.arraySize} elements";            
            float width = DrawerHelper.CalculateLabelWidth(countText);
            EditorGUI.LabelField(rect.AppendRight().MoveX(-width).WithWidth(width), countText, style);
        }

        protected override string NoneElementText(string name)
        {
            return base.NoneElementText("Priority Queue");
        }
    }
}