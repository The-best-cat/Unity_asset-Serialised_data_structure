using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class TupleDrawer : PropertyDrawer
    {
        private Dictionary<string, float> tupleHeights = new Dictionary<string, float>();        

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {                   
            EditorGUI.BeginProperty(position, label, property);
            DrawTuple(position, property, label);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetTupleHeight(property);        
        }

        public void DrawTuple(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0;
            var labels = GetLabels();
            EditorGUI.BeginProperty(position, label, property);

            if (labels != null)
            {
                Rect foldout = position.WithHeight(EditorGUIUtility.singleLineHeight);
                property.isExpanded = EditorGUI.Foldout(foldout, property.isExpanded, label);          
                if (property.isExpanded)
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
            string key = DrawerHelper.GetPropertyKey(property);
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
            string key = DrawerHelper.GetPropertyKey(property);
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
            int realCount = 4;
            int left = 1, right = 8;
            while (left < right)
            {
                int middle = (left + right) / 2;
                if (property.FindPropertyRelative("Item" + middle) != null)
                {
                    realCount = middle;
                    left = middle + 1;
                }
                else
                {
                    right = middle;
                }
            }
            return realCount == count;
        }

        public string[] GetLabels()
        {
            SerializedTupleAttribute attribute = fieldInfo.GetCustomAttributes(typeof(SerializedTupleAttribute), false).FirstOrDefault() as SerializedTupleAttribute;
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
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,>), true)]
    public class PairDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,>), true)]
    public class TripletDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,,>), true)]
    public class QuartetDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,,,>), true)]
    public class QuintetDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,,,,>), true)]
    public class SextetDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,,,,,>), true)]
    public class SeptetDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(SerializedTuple<,,,,,,,>), true)]
    public class Octet : TupleDrawer
    {
    }
}
