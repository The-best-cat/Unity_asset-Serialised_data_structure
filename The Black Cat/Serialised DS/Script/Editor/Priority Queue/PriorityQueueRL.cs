using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class PriorityQueueRL : RLDrawer
    {
        public PriorityQueueRL(SerializedProperty property, GUIContent label) : base(property, label)
        {
            reorderableList.draggable = false;
        }

        public override float GetPropertyHeight()
        {
            if (listProperty == null)
            {
                return 0;
            }
            if (helper.IsRLExpanded(listProperty))
            {
                return reorderableList.GetHeight() - EditorGUIUtility.singleLineHeight;
            }
            return 20;
        }

        protected override string NoneElementText(string name)
        {
            return base.NoneElementText("Priority Queue");
        }
    }
}