using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class SerializedDictionaryExample : MonoBehaviour
    {
        [SerializedCollectionLabel("Demon Levels (Custom Label + Duplicate Keys Example)")]
        [SerializedDictionary("Demon Name", "Hazard Level")]
        public SerializedDictionary<string, Levels> demonLevels;

        public SerializedDictionary<GameObject, string> nullGameObjectKeysExample;        
    }

    public enum Levels
    {
        C,
        B,
        A,
        S,
        SS,
        SSS
    }
}