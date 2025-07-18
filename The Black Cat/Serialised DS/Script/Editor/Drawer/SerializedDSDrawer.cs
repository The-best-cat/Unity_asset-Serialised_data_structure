using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class SerialisedDSDrawer : PropertyDrawer
    {        
        private Dictionary<string, RLDrawer> reorderableLists = new Dictionary<string, RLDrawer>();        

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GetRL(property, label).OnGUI(position, label);            
        }        

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return GetRL(property, label).GetPropertyHeight();
        }

        private RLDrawer ValidateRL(RLDrawer drawer, SerializedProperty property, GUIContent label)
        {         
            if (drawer == null || !drawer.ValidateSerialisedObject(property.serializedObject))
            {
                drawer = CreateRL(property, label);
                RegisterRL(property, drawer);
            }
            return drawer;
        }

        protected virtual RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new RLDrawer(property, label, fieldInfo);
        }

        private void RegisterRL(SerializedProperty property, RLDrawer rl)
        {
            string key = DrawerHelper.GetPropertyKey(property);
            if (!reorderableLists.ContainsKey(key))
            {
                reorderableLists.Add(key, rl);
            }
            else
            {
                reorderableLists[key] = rl;
            }
        }

        private RLDrawer GetRL(SerializedProperty property, GUIContent label)
        {
            string key = DrawerHelper.GetPropertyKey(property);
            reorderableLists.TryGetValue(key, out RLDrawer drawer);
            drawer = ValidateRL(drawer, property, label);
            return drawer;
        }
    }

    [CustomPropertyDrawer(typeof(SerializedQueue<>), true)]
    public class SerializedQueueDrawer : SerialisedDSDrawer
    {
        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new QueueRL(property, label, fieldInfo);
        }
    }

    [CustomPropertyDrawer(typeof(SerializedStack<>), true)]
    public class SerializedStackDrawer : SerialisedDSDrawer
    {
        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new StackRL(property, label, fieldInfo);
        }
    }
}