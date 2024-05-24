using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;


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



#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        stroyModels = Resources.LoadAll<StoryModel>(""); //Resource 폴더 아래 모든 StoryModel 을 불러 온다. 
    }
#endif

    public void StoryShow(int number)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        //StorySystem.Instace.currentStoryModel = tempStoryMoels;
        //StorySystem.Instance.CoShowText();
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