using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerializedPriorityQueueExample : MonoBehaviour
    {
        public SerializedPriorityQueue<char, int> charPriorityQueue = new();        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                charPriorityQueue.Enqueue((char)('A' + Random.Range(0, 26)), Random.Range(0, 200));
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (!charPriorityQueue.IsEmpty)
                {
                    charPriorityQueue.Dequeue();                    
                }
            }
        }
    }
}


