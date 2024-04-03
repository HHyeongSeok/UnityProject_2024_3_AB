using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace STORYGAME
{
    [CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryTableObject")]
    public class StoryTableObject : ScriptableObject
    {
        public int storyNumver;
        public Enums.StoryType storyType;
        public bool storyDone;

        [TextArea(10, 10)]
        public string storyText;
        public List<Option> options = new List<Option>();

        [System.Serializable]
        public class Option
        {
            public string optionText;
            public string buttonText;
            public EventCheck eventCheck;
        }
        public class EventCheck
        {
            public int checkValue;
            public Enums.EvenType evenType;
            public List<Result> successResult = new List<Result>();
            public List<Result> failResult = new List<Result>();
        }

        [System.Serializable]
        public class Result
        {
            public Enums.ResultType resultType;
            public int value;
            public Stats stats;
        }
    }
}