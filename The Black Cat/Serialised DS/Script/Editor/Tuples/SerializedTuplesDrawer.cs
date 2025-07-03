using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    public class TupleDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {                   
            EditorGUI.BeginProperty(position, label, property);
            SerialisedDSHelper.Instance.DrawTuple(this, position, property, label);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return SerialisedDSHelper.Instance.GetTupleHeight(property);        
        }
    }

    [CustomPropertyDrawer(typeof(Pair<,>), true)]
    public class PairDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(Triplet<,,>), true)]
    public class TripletDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(Quartet<,,,>), true)]
    public class QuartetDrawer : TupleDrawer
    {
    }

    [CustomPropertyDrawer(typeof(Quintet<,,,,>), true)]
    public class QuintetDrawer : TupleDrawer
    {
    }
}
