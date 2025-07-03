using System;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class SerializedTupleAttribute : Attribute
    {
        public readonly string[] labels;

        public SerializedTupleAttribute(string A)
        {
            labels = new string[1] { A };
        }

        public SerializedTupleAttribute(string A, string B)
        {
            labels = new string[2] { A, B };
        }

        public SerializedTupleAttribute(string A, string B, string C)
        {
            labels = new string[3] { A, B, C };
        }

        public SerializedTupleAttribute(string A, string B, string C, string D)
        {
            labels = new string[4] { A, B, C, D };
        }

        public SerializedTupleAttribute(string A, string B, string C, string D, string E)
        {
            labels = new string[5] { A, B, C, D, E };
        }
    }
}