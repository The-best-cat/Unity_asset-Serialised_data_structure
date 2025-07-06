using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class IndividualIdentity
    {
        public string firstName;
        public string lastName;
        public int id;
    }

    public class PersonIdentityEqualityComparer<T> : IEqualityComparer<T> where T : IndividualIdentity
    {
        public bool Equals(T x, T y)
        {
            if (x is IndividualIdentity personX && y is IndividualIdentity personY)
            {
                return personX.id == personY.id;
            }
            return false;
        }

        public int GetHashCode(T obj)
        {
            int hash = 13;
            hash = hash * 7 ^ ((obj as IndividualIdentity)?.id.GetHashCode() ?? 0);
            return hash;
        }
    }
}