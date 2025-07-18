using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class RLDrawer
    {
        public SerializedObject SerializedObject { get; protected set; }

        protected bool hasCustomDrawer;
        protected GUIContent header;        

        protected ReorderableList reorderableList;
        protected ReorderableList unexpandedList;
        protected SerializedProperty listProperty;        

        public RLDrawer(SerializedProperty property, GUIContent label, FieldInfo info)
        {            
            hasCustomDrawer = DrawerHelper.HasCustomDrawer(property);            
            SerializedObject = property.serializedObject;

            reorderableList = MakeRL(property);
            unexpandedList = new ReorderableList(DrawerHelper.EmptyList, typeof(int), false, true, false, false);

            listProperty = reorderableList.serializedProperty;            
            if (listProperty == null)
            {
                Debug.LogError("Drawing failed! If you are storing lists, write a wrapper for it and store the wrapper instead. Remember to make the wrapper serialisable!");
                return;
            }

            reorderableList.drawHeaderCallback += OnDrawHeader;            
            reorderableList.elementHeightCallback += OnGetElementHeight;
            reorderableList.drawElementCallback += OnDrawElement;
            reorderableList.drawNoneElementCallback += OnDrawNoneElement;             

            unexpandedList.elementHeight = -8;
            unexpandedList.drawHeaderCallback += OnDrawUnexpandedHeader;

            var labelAttribute = info.GetCustomAttribute<SerializedCollectionLabelAttribute>();
            string labelText = labelAttribute?.label ?? label.text;
            header = new GUIContent(labelText, label.tooltip);
        }

        public bool ValidateSerialisedObject(SerializedObject so)
        {
            return so != null && SerializedObject != null && so == SerializedObject;
        }

        public void OnGUI(Rect position, GUIContent label)
        {
            if (listProperty == null)
            {
                return;
            }

            EditorGUI.BeginChangeCheck();

            reorderableList.draggable = !Application.isPlaying;
            if (listProperty.isExpanded)
            {
                reorderableList.DoList(position);
            }
            else
            {
                unexpandedList.DoList(position);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(SerializedObject.targetObject, "Change in " + DrawerHelper.GetPropertyKey(listProperty));
                listProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected virtual ReorderableList MakeRL(SerializedProperty property)
        {
            return new ReorderableList(property.serializedObject, property.FindPropertyRelative("serialisedList"), true, true, false, false);
        }

        public virtual float GetPropertyHeight()
        {            
            if (listProperty == null)
            {
                return 0;
            }
            if (listProperty.isExpanded)
            {
                return reorderableList.GetHeight();
            }
            return 20;
        }

        protected virtual void OnDrawHeader(Rect rect)
        {
            EditorGUI.BeginProperty(rect, header, listProperty);

            EditorGUI.LabelField(rect, header, EditorStyles.boldLabel);

            DrawElementCount(rect);

            listProperty.isExpanded = EditorGUI.Foldout(rect.ExpandLeft(7), listProperty.isExpanded, GUIContent.none, true);            

            EditorGUI.EndProperty();
        }

        protected virtual void DrawElementCount(Rect rect)
        {
            Rect r_count = rect.AppendRight().MoveX(-30).WithWidth(30);
            listProperty.arraySize = EditorGUI.DelayedIntField(r_count, listProperty.arraySize);
        }

        protected virtual void OnDrawUnexpandedHeader(Rect rect)
        {
            OnDrawHeader(rect);
        }

        protected virtual void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            if (index >= listProperty.arraySize)
            {
                return;
            }

            rect = rect.CutRight(46);            
            DrawElement(rect, index, isActive, isFocused);
            DrawRemoveButton(rect, index);
        }

        protected virtual void DrawElement(Rect rect, int index, bool inActive, bool isFocused)
        {
            var element = listProperty.GetArrayElementAtIndex(index);
            if (element.hasVisibleChildren && !hasCustomDrawer)
            {
                rect = rect.CutLeft(11);
            }
            EditorGUI.PropertyField(rect.MoveY(2).ExpandBottom(2), element, true);
        }

        protected virtual void DrawRemoveButton(Rect rect, int index)
        {
            if (GUI.Button(rect.AppendRight(2).MoveY(2).WithWidth(45).WithHeight(EditorGUIUtility.singleLineHeight), "－"))
            {
                listProperty.DeleteArrayElementAtIndex(index);
                listProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        protected virtual float OnGetElementHeight(int index)
        {
            var element = listProperty.GetArrayElementAtIndex(index);
            return EditorGUI.GetPropertyHeight(element, true) + 2;
        }

        protected void OnDrawNoneElement(Rect rect)
        {
            EditorGUI.LabelField(rect, NoneElementText(""));
        }

        protected virtual string NoneElementText(string name)
        {
            return name + " is Empty";
        }
    }
}
