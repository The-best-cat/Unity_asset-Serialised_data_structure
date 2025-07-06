using System;
using System.Diagnostics;

namespace TheBlackCat.SerialisedDS
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SerializedCollectionLabelAttribute : Attribute
    {
        public readonly string label;

        public SerializedCollectionLabelAttribute(string label)
        {
            this.label = label;
        }
    }
}