using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using Unity.VisualScripting;


#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEdiot : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameSystem gameSystem = (GameSystem)target;

        //Reset Story Models 버튼 생성 
        if (GUILayout.Button("Rest Stroy Modes"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;                      //간단한 싱글톤 화

    private void Awake()
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STROYSHOW,
        WAITSELECT,
        STORYEND
    }

    public Stats stats;
    public GAMESTATE currentState;
    public int currentStoryIndex = 1;
    public StoryModel[] stroyModels;

    public void Start()
    {
        ChangeState(GAMESTATE.STROYSHOW);
    }


#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        stroyModels = Resources.LoadAll<StoryModel>(""); //Resource 폴더 아래 모든 StoryModel 을 불러 온다. 
    }
#endif


    public void ChangeState(GAMESTATE temp)                 //게임 상태 변경 함수 추가 (인수로 게임상태 열거형)
    {
        currentState = temp;

        if (currentState == GAMESTATE.STROYSHOW)
        {
            StoryShow(currentStoryIndex);                   //스토리 재생
        }
    }

    public void ApplyChoice(StoryModel.Reuslt result)           //결과 값을 통한 게임 시스템 진행 시켜주는 함수
    {
        switch (result.resultType)
        {
            case StoryModel.Reuslt.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Reuslt.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Reuslt.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STROYSHOW);
                ChangeStats(result);
                break;

            case StoryModel.Reuslt.ResultType.GoToRandomStory:
                RandomStory();
                ChangeState(GAMESTATE.STROYSHOW);
                ChangeStats(result);
                break;
            default:
                Debug.LogError("Unknown type");
                break;
        }
    }

    public void ChangeStats(StoryModel.Reuslt result)           //게임 스텟 변경 (스토리 모델의 결과값)
    {
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

        if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
        if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
        if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

        if (result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
        if (result.stats.consitution > 0) stats.consitution += result.stats.consitution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;
    }

    public void StoryShow(int number)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        StorySystem.instance.currentStoryModel = tempStoryModels;
        StorySystem.instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for (int i = 0; i < stroyModels.Length; i++)             //for 문으로 StroyModel 을 검색하여 Number 와 같은 스토리 번호로 스토리 모델을 찾아 반환한다. 
        {
            if (stroyModels[i].storyNumber == number)
            {
                tempStoryModels = stroyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel> storyModelList = new List<StoryModel>();

        for (int i = 0; i < stroyModels.Length; i++)             //for 문으로 StroyModel 을 검색하여 Main인 경우만 추출. 
        {
            if (stroyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
            {
                storyModelList.Add(stroyModels[i]);
            }
        }

        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)]; //리스트에서 랜덤으로 하나 선택
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }

}