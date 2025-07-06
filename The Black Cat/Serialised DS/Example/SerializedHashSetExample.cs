using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerializedHashSetExample : MonoBehaviour
    {        
        public SerializedHashSet<int> mySet;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                mySet.Add(Random.Range(1, 11));
            }
        }
    }
}