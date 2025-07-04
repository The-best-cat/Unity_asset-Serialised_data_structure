using System.Collections.Generic;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerialisedTuplesExample : MonoBehaviour
    {
        [SerializedTuple("Name", "ID")]
        public SerializedTuple<string, int> NameWithID;

        [SerializedTuple("Name", "ID", "Classroom")]
        public SerializedTuple<string, int, string> NameWithIDAndClassroom;

        [SerializedTuple("Name", "ID", "Classroom", "Score")]
        public SerializedTuple<string, int, string, float> NameWithIDClassroomAndScore;

        [SerializedTuple("Name", "ID", "Classroom", "Score", "Passed")]
        public SerializedTuple<string, int, string, float, bool> NameWithIDClassroomScoreAndPassed;
    }
}