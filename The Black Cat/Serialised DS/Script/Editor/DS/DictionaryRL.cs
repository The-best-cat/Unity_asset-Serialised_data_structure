using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class DictionaryRL : RLDrawer
    {
        private string keyLabel;
        private string valueLabel;
        private Rect middleBoarder;
        private Color32 boarderColour = new Color32(36, 36, 36, 255);
        private Color32 red = new Color32(255, 32, 20, 255);
        private Color32 yellow = new Color32(255, 187, 0, 255);
        private IKeyOccurenceHelper keyOccurenceHelper;

        public DictionaryRL(SerializedProperty property, GUIContent label, FieldInfo info) : base(property, label, info)
        {
            reorderableList.drawFooterCallback += OnDrawFooter;
            reorderableList.headerHeight += 18;

            var labelAttribute = info.GetCustomAttribute<SerializedDictionaryAttribute>();
            keyLabel = labelAttribute?.key ?? "Key";
            valueLabel = labelAttribute?.value ?? "Value";

            GetKeyOccurenceHelper();
        }

        protected override void OnDrawHeader(Rect rect)
        {
            rect.height -= 18;
            base.OnDrawHeader(rect);

            Rect r_labels = rect.AppendBottom(1);
            Rect label_key = r_labels.WithWidth(r_labels.width / 2);
            Rect label_value = label_key.AppendRight().WithWidth(r_labels.width - label_key.width);

            GUIStyle centred = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUI.LabelField(label_key, keyLabel, centred);
            EditorGUI.LabelField(label_value, valueLabel, centred);

            Rect r_leftBoarder = r_labels.MoveX(-6).WithWidth(1);
            Rect r_rightBoarder = r_labels.AppendRight(5).WithWidth(1);
            middleBoarder = rect.WithY(r_leftBoarder.y).WithX((r_leftBoarder.x + r_rightBoarder.x) / 2).WithWidth(1).WithHeight(reorderableList.headerHeight);

            EditorGUI.DrawRect(r_leftBoarder, boarderColour);
            EditorGUI.DrawRect(r_rightBoarder, boarderColour);
            EditorGUI.DrawRect(rect.ExpandLeft(6).ExpandRight(7).AppendBottom(18).WithHeight(1), boarderColour);
            EditorGUI.DrawRect(middleBoarder, boarderColour);
        }

        protected override void OnDrawElement(Rect position, int index, bool isActive, bool isFocused)
        {
            if (index >= listProperty.arraySize)
            {
                return;
            }

            var element = listProperty.GetArrayElementAtIndex(index);
            var key = element.FindPropertyRelative("Key");
            var value = element.FindPropertyRelative("Value");

            if (GUI.Button(position.AppendRight().MoveX(-44).MoveY(2).WithWidth(45).WithHeight(EditorGUIUtility.singleLineHeight), "¡Ð"))
            {
                listProperty.DeleteArrayElementAtIndex(index);
                listProperty.serializedObject.ApplyModifiedProperties();
                return;
            }

            var occurences = GetKeyOccurenceHelper().GetKeyOccurence(GetKeyOccurenceHelper().GetKeyAt(index));
            GUIStyle warningStyle = new GUIStyle(EditorStyles.foldout);
            Color32 orgColour = warningStyle.normal.textColor;
            
            if (occurences?.Count > 1)
            {                
                warningStyle.normal.textColor = occurences[0] == index ? yellow : red;                                
            }
            warningStyle.normal.textColor = GetKeyOccurenceHelper().IsValidKeyAt(index) ? warningStyle.normal.textColor : red;
            warningStyle.fontStyle = orgColour != warningStyle.normal.textColor ? FontStyle.Bold : warningStyle.fontStyle;

            Rect foldout = position.MoveX(11).MoveY(2).WithHeight(EditorGUIUtility.singleLineHeight);
            element.isExpanded = EditorGUI.Foldout(foldout, element.isExpanded, "Element " + index, true, warningStyle);
            if (element.isExpanded)
            {
                Rect r_key = foldout.AppendBottom(2).WithHeight(EditorGUI.GetPropertyHeight(key));
                r_key.width = middleBoarder.x - r_key.x - 2;
                Rect r_value = r_key.WithX(middleBoarder.x + 3).WithWidth(position.width - r_key.width - 15);

                if (key.hasVisibleChildren && !hasCustomDrawer)
                {
                    r_key = r_key.CutLeft(11);
                }
                if (value.hasVisibleChildren && !hasCustomDrawer)
                {
                    r_value = r_value.CutLeft(12);
                }

                orgColour = GUI.color;
                GUI.color = warningStyle.normal.textColor;
                EditorGUI.PropertyField(r_key.CutRight(2), key, GUIContent.none, true);
                GUI.color = orgColour;

                EditorGUI.PropertyField(r_value.CutLeft(2), value, GUIContent.none, true);
            }

            Rect mid = middleBoarder.WithY(foldout.y).ExpandTop(2).WithHeight(OnGetElementHeight(index) + 2);
            EditorGUI.DrawRect(mid, boarderColour);
            if (index == 0)
            {
                EditorGUI.DrawRect(mid.MoveY(-4).WithHeight(4), boarderColour);
            }
            else if (index == listProperty.arraySize - 1)
            {
                EditorGUI.DrawRect(mid.MoveY(OnGetElementHeight(index) + 2).WithHeight(4), boarderColour);
            }
        }

        protected override void OnDrawUnexpandedHeader(Rect rect)
        {
            base.OnDrawHeader(rect);
        }

        protected override float OnGetElementHeight(int index)
        {
            if (index >= listProperty.arraySize)
            {
                return 0;
            }

            var element = listProperty.GetArrayElementAtIndex(index);
            if (element.isExpanded)
            {
                var key = element.FindPropertyRelative("Key");
                var value = element.FindPropertyRelative("Value");
                return Mathf.Max(EditorGUI.GetPropertyHeight(key), EditorGUI.GetPropertyHeight(value)) + EditorGUIUtility.singleLineHeight + 4;
            }
            return base.OnGetElementHeight(index);
        }

        private void OnDrawFooter(Rect rect)
        {
            if (GUI.Button(rect.AppendRight().MoveX(-51).WithWidth(45), "¡Ï"))
            {
                listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
                listProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        private IKeyOccurenceHelper GetKeyOccurenceHelper()
        {
            if (keyOccurenceHelper == null)
            {
                var dictionary = DrawerHelper.GetPropertyTargetObject(listProperty, SerializedObject.targetObject);
                var helperField = dictionary.GetType().GetProperty("KeyOccurenceHelper", BindingFlags.NonPublic | BindingFlags.Instance);
                keyOccurenceHelper = (IKeyOccurenceHelper)helperField.GetValue(dictionary);
                keyOccurenceHelper.CalculateKeyOccurence();
            }
            return keyOccurenceHelper;
        }

        protected override string NoneElementText(string name)
        {
            return base.NoneElementText("Dictionary");
        }
    }
}