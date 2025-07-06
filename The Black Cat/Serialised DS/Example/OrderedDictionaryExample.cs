using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class OrderedDictionaryExample : MonoBehaviour
    {       
        [SerializedDictionary("Individual", "Description")]
        public OrderedDictionary<IndividualIdentity, string> peopleDescriptions = 
            new (new PersonIdentityEqualityComparer<IndividualIdentity>());
    }
}   