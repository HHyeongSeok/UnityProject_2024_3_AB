using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Enums
    {
        public enum StoryType       //스토리 타입
        {
            MAIN,
            SUB,
            SERIAL
        }

        public enum EvenType        //이벤트 발생시 체크
        {
            NONE,
            GoToBattle = 100,
            CheckSTR = 1000,
        }

        public enum ResultType      //이벤트 결과 열거
        {
            AddExperience,
            GoToNextStory,
            GoToRandomStory,
        }
    }



    [System.Serializable]
    public class Stats
    {
        //체력과 기력
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        //기본 스텟 설정
        public int strength;        //STR
        public int dexterity;       //DEX
        public int consitution;    //CON
        public int Intelligence;    //INT
        public int wisdom;          //WIS
        public int charisma;        //CHA
    }
