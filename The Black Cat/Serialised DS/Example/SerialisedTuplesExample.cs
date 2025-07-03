using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheBlackCat.SerialisedDS
{
    public class SerialisedTuplesExample : MonoBehaviour
    {
        [SerializedTuple("Name", "ID")]
        public Pair<string, int> NameWithID;

        [SerializedTuple("Name", "ID", "Classroom")]
        public Triplet<string, int, string> NameWithIDAndClassroom;

        [SerializedTuple("Name", "ID", "Classroom", "Score")]
        public Quartet<string, int, string, float> NameWithIDClassroomAndScore;

        [SerializedTuple("Name", "ID", "Classroom", "Score", "Passed")]
        public Quintet<string, int, string, float, bool> NameWithIDClassroomScoreAndPassed;
    }
}