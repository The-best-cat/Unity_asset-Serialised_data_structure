using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class SerialisedDSDrawer : PropertyDrawer
    {
        private RLDrawer rl;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ValidateRL(property, label);
            rl.OnGUI(position, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ValidateRL(property, label);
            return rl.GetPropertyHeight();
        }

        protected void ValidateRL(SerializedProperty property, GUIContent label)
        {
            rl = SerialisedDSHelper.Instance.GetRL(property);            
            if (rl == null || !rl.ValidateSerialisedObject(property.serializedObject))
            {
                rl = CreateRL(property, label);
                SerialisedDSHelper.Instance.RegisterRL(property, rl);
            }
        }

        protected virtual RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new RLDrawer(property, label);
        }
    }

    [CustomPropertyDrawer(typeof(SerializedQueue<>), true)]
    public class SerializedQueueDrawer : SerialisedDSDrawer
    {
        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new QueueRL(property, label);
        }
    }

    [CustomPropertyDrawer(typeof(SerializedStack<>), true)]
    public class SerializedStackDrawer : SerialisedDSDrawer
    {
        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new StackRL(property, label);
        }
    }

    [CustomPropertyDrawer(typeof(SerializedPriorityQueue<,>), true)]
    public class SerializedPriorityQueueDrawer : SerialisedDSDrawer
    {
        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new PriorityQueueRL(property, label);
        }
    }
}