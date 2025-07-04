using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class QueueRL : RLDrawer
    {
        public QueueRL(SerializedProperty property, GUIContent label) : base(property, label)
        {
            reorderableList.drawFooterCallback += OnDrawFooter;
        }

        private void OnDrawFooter(Rect rect)
        {
            Rect r_dequeue = rect.AppendRight().MoveX(-51).WithWidth(45);
            if (GUI.Button(r_dequeue, "¡Ð"))
            {
                if (listProperty.arraySize > 0)
                {
                    listProperty.DeleteArrayElementAtIndex(0);
                    listProperty.serializedObject.ApplyModifiedProperties();
                }
            }
            if (GUI.Button(r_dequeue.MoveX(-48).WithWidth(45), "¡Ï"))
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