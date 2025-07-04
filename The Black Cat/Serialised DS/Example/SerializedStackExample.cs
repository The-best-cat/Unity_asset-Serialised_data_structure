using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerializedStackExample : MonoBehaviour
    {        
        public SerializedStack<int> idStack;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                int randomValue = Random.Range(0, 100);
                idStack.Push(randomValue);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (!idStack.IsEmpty)
                {
                    idStack.Pop();
                }
            }
        }
    }
}