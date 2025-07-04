using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerializedQueueExample : MonoBehaviour
    {                
        public SerializedQueue<PersonIdentify> queuingPeople;        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                queuingPeople.Enqueue(new PersonIdentify());
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (!queuingPeople.IsEmpty)
                {
                    queuingPeople.Dequeue();
                }               
            }
        }
    }
}