using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class StackRL : RLDrawer
    {
        public StackRL(SerializedProperty property, GUIContent label, FieldInfo info) : base(property, label, info)
        {
            reorderableList.drawFooterCallback += OnDrawFooter;
        }

        private void OnDrawFooter(Rect rect)
        {
            Rect r_pop = rect.AppendRight().MoveX(-51).WithWidth(45);
            if (GUI.Button(r_pop, "¡Ð"))
            {
                if (listProperty.arraySize > 0)
                {
                    listProperty.DeleteArrayElementAtIndex(listProperty.arraySize - 1);
                    listProperty.serializedObject.ApplyModifiedProperties();
                }
            }
            if (GUI.Button(r_pop.MoveX(-48).WithWidth(45), "¡Ï"))
            {
                listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
                listProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected override string NoneElementText(string name)
        {
            return base.NoneElementText("Stack");
        }
    }
}