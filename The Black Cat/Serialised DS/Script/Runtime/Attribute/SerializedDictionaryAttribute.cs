using System;
using System.Diagnostics;

namespace TheBlackCat.SerialisedDS
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SerializedDictionaryAttribute : Attribute
    {
        public readonly string key;
        public readonly string value;

        public SerializedDictionaryAttribute(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }
}