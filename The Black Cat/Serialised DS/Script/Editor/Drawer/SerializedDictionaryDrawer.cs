using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TheBlackCat.SerialisedDS.Editor
{
    [CustomPropertyDrawer(typeof(SerializedDictionary<,>), true)]
    [CustomPropertyDrawer(typeof(OrderedDictionary<,>), true)]
    public class SerializedDictionaryDrawer : SerialisedDSDrawer
    {
        private readonly Dictionary<string, (string, string)> labels = new Dictionary<string, (string, string)>();

        protected override RLDrawer CreateRL(SerializedProperty property, GUIContent label)
        {
            return new DictionaryRL(property, label, fieldInfo);
        }

        public (string, string) GetLabels(SerializedProperty property)
        {
            if (labels.TryGetValue(DrawerHelper.GetPropertyKey(property), out var dictLabels))
            {
                return dictLabels;
            }

            SerializedDictionaryAttribute attribute = fieldInfo.GetCustomAttributes(typeof(SerializedDictionaryAttribute), false).FirstOrDefault() as SerializedDictionaryAttribute;
            if (attribute != null)
            {
                dictLabels = (attribute.key, attribute.value);
                labels.Add(DrawerHelper.GetPropertyKey(property), dictLabels);
            }
            return dictLabels;
        }
    }
}
