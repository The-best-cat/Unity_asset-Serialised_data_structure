using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerializedQueueExample : MonoBehaviour
    {                
        public SerializedQueue<IndividualIdentity> queuingPeople;        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {                
                if (!queuingPeople.IsEmpty)
                {
                    var person = queuingPeople.Dequeue();
                    Debug.Log($"Individual named {person.firstName} {person.lastName} is registered with ID {person.id}.");
                }
                else
                {
                    Debug.Log("No more people is queuing.");
                }
            }
        }        
    }
}