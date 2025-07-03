using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class SerialisedDSHelper : ScriptableObject
    {
        private const string PATH = "Assets/The Black Cat/Serialised DS/Script/Editor/Helper/SerialisedDSHelper.asset";

        private Dictionary<string, RLDrawer> reorderableLists = new Dictionary<string, RLDrawer>();
        private Dictionary<string, bool> isDSExpanded = new Dictionary<string, bool>();

        private Dictionary<string, float> tupleHeights = new Dictionary<string, float>();
        private Dictionary<string, bool> isTupleExpanded = new Dictionary<string, bool>();

        private static SerialisedDSHelper instance;

        public static SerialisedDSHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    if (AssetDatabase.LoadAssetAtPath<SerialisedDSHelper>(PATH) != null)
                    {
                        instance = AssetDatabase.LoadAssetAtPath<SerialisedDSHelper>(PATH);
                    }
                    else
                    {
                        instance = CreateInstance<SerialisedDSHelper>();
                        AssetDatabase.CreateAsset(instance, PATH);
                        AssetDatabase.SaveAssets();
                    }                    
                }
                return instance;
            }
        }

        public RLDrawer GetRL(SerializedProperty property)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (reorderableLists.TryGetValue(key, out RLDrawer drawer))
            {
                return drawer;
            }            
            return null;
        }

        public void RegisterRL(SerializedProperty property, RLDrawer rl)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (!reorderableLists.ContainsKey(key))
            {                
                reorderableLists.Add(key, rl);
            }
            else
            {
                reorderableLists[key] = rl;
            }
        }

        public bool IsRLExpanded(SerializedProperty property)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (!isDSExpanded.TryGetValue(key, out bool expanded))
            {
                expanded = false;                
            }
            return expanded;
        }

        public void SetRLExpanded(SerializedProperty property, bool value)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (isDSExpanded.ContainsKey(key))
            {
                isDSExpanded[key] = value;
            }
            else
            {
                isDSExpanded.Add(key, value);
            }
        }

        public void DrawTuple(PropertyDrawer drawer, Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0;
            var labels = GetLabels(drawer);
            EditorGUI.BeginProperty(position, label, property);

            if (labels != null)
            {
                Rect foldout = position.WithHeight(EditorGUIUtility.singleLineHeight);
                SetTupleExpanded(property, EditorGUI.Foldout(foldout, IsTupleExpanded(property), label));
                if (IsTupleExpanded(property))
                {
                    EditorGUI.indentLevel++;
                    Rect r_item = foldout;

                    if (labels == null || !ValidateAttribute(labels.Length, property))
                    {
                        throw new System.Exception("Number of labels doesn't match with the number of items in the tuple.");
                    }

                    for (int i = 0; i < labels.Length; i++)
                    {
                        var item = property.FindPropertyRelative("Item" + (i + 1));
                        r_item = r_item.AppendBottom(2).WithHeight(EditorGUI.GetPropertyHeight(item));
                        DrawTupleField(r_item, item, labels?[i] ?? "");
                        height += r_item.height + 2;
                    }

                    EditorGUI.indentLevel--;
                }
                
                height += foldout.height;
            }
            else
            {                
                EditorGUI.PropertyField(position, property, label, true);
                height = EditorGUI.GetPropertyHeight(property, label, true);
            }

            EditorGUI.EndProperty();
            SetTupleHeight(property, height);
        }

        public float GetTupleHeight(SerializedProperty property)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (tupleHeights.TryGetValue(key, out float height))
            {
                return height;
            }
            float newHeight = EditorGUI.GetPropertyHeight(property, true);
            tupleHeights[key] = newHeight;
            return newHeight;
        }

        private void SetTupleHeight(SerializedProperty property, float height)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (tupleHeights.ContainsKey(key))
            {
                tupleHeights[key] = height;
            }
            else
            {
                tupleHeights.Add(key, height);
            }
        }

        private bool ValidateAttribute(int count, SerializedProperty property)
        {
            int realCount = 0;
            for (int i = 1; i <= 5; i++)
            {
                if (property.FindPropertyRelative($"Item{i}") != null)
                {
                    realCount++;
                }
            }
            return realCount == count;
        }

        public string[] GetLabels(PropertyDrawer drawer)
        {
            SerializedTupleAttribute attribute = drawer.fieldInfo.GetCustomAttributes(typeof(SerializedTupleAttribute), false).FirstOrDefault() as SerializedTupleAttribute;            
            return attribute?.labels;
        }

        public void DrawTupleField(Rect position, SerializedProperty tupleProp, string display)
        {
            Debug.Assert(tupleProp != null);

            if (string.IsNullOrEmpty(display))
            {
                display = tupleProp.displayName;
            }

            EditorGUI.PropertyField(position, tupleProp, new GUIContent(display), true);
        }

        private void SetTupleExpanded(SerializedProperty property, bool value)
        {
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (isTupleExpanded.ContainsKey(key))
            {
                isTupleExpanded[key] = value;
            }
            else
            {
                isTupleExpanded.Add(key, value);
            }
        }

        public bool IsTupleExpanded(SerializedProperty property)
        {            
            string key = property.propertyPath + "." + property.serializedObject.targetObject.GetInstanceID();
            if (!isTupleExpanded.TryGetValue(key, out bool expanded))
            {
                expanded = false;
                isTupleExpanded.Add(key, expanded);
            }
            return expanded;
        }
    }
}