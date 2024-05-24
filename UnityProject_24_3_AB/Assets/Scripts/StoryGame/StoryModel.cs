using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStroy", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{

    public int storyNumber;
    public Texture2D mainImage;

    public enum STORYTYPE
    {
        MAIN,
        SUB,
        SERIAL
    }

    public STORYTYPE storyType;
    public bool storyDone;

    [TextArea(10, 10)]
    public string storyText;

    public Option[] options;            //선택지 배열 

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;       //선택지 버튼의 이름

        public EventCheck eventCheck;
    }


    [System.Serializable]
    public class EventCheck
    {
        public int checkvalue;
        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType eventType;

        public Reuslt[] sucessResult;           //선택지에 대한 효과 배열
        public Reuslt[] failResult;
    }

    [System.Serializable]
    public class Reuslt                     //결과값 정보 데이터
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoToRandomStory
        }

        public ResultType resultType;
        public int value;
        public Stats stats;
    }
}