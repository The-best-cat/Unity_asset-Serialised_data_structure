using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public struct SerializedKeyValuePair<K, V>
    {
        public K Key;
        public V Value;

        public SerializedKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public void Deconstruct(out K key, out V value)
        {
            key = Key;
            value = Value;
        }

        public static implicit operator KeyValuePair<K, V>(SerializedKeyValuePair<K, V> pair)
        {
            return new KeyValuePair<K, V>(pair.Key, pair.Value);
        }

        public static implicit operator SerializedKeyValuePair<K, V>(KeyValuePair<K, V> pair)
        {
            return new SerializedKeyValuePair<K, V>(pair.Key, pair.Value);
        }
    }
}