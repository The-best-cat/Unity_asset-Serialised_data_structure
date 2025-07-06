using System.Collections.Generic;

namespace TheBlackCat.SerialisedDS
{
    public interface IKeyOccurenceHelper
    {
        List<int> GetKeyOccurence(object key);
        void CalculateKeyOccurence();
        object GetKeyAt(int index);
        bool IsValidKeyAt(int index);
        bool IsValidKey(object key);
    }
}