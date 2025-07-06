using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    [System.Serializable]
    public class ListWrapper<T>
    {
        public List<T> List;

        public ListWrapper()
        {
            List = new List<T>();
        }

        public ListWrapper(IEnumerable<T> collection)
        {
            List = new List<T>(collection);
        }

        public ListWrapper(int capacity)
        {
            List = new List<T>(capacity);
        }        

        public static explicit operator List<T>(ListWrapper<T> wrapper)
        {
            return wrapper.List;
        }

        public static explicit operator ListWrapper<T>(List<T> list)
        {
            return new ListWrapper<T>(list);
        }
    }
}