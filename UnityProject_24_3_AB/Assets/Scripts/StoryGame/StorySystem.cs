using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StorySystem : MonoBehaviour
{
    public static StorySystem instance;             //간단한 싱글톤화 

    public StoryModel currentStoryModel;
    public enum TEXTSYSTEM
    {
        DOING,
        SELECT,
        DONE
    }

    public float delay = 0.1f;                      //각 글자가 나타나는데 걸리는 시간
    public string fullText;                         //전체 표시할 텍스트
    private string currentText = "";                //현재까지 표시된 텍스트 

    public Text textComponent;                      //Text 컴포넌트
    public Text storyIndex;                         //지금 스토리 번호

    public Image imageComponent;                    //보여진 이미지 컴포넌트

    public Button[] buttonWay = new Button[3];
    public Text[] buttonWayText = new Text[3];

    private void Awake()
    {
        instance = this;
    }

    public void OnWayClick(int index)               //버튼이 눌렸을때 해당 설정된 index를 받아온다. 
    {
        bool CheckEventTypeNone = false;            //기본으로 NONE 일 는 성공이라고 판단
        StoryModel playStoryModel = currentStoryModel;
        Debug.Log(index);

        if (playStoryModel.options[index].eventCheck.eventType == StoryModel.EventCheck.EventType.NONE)
        {
            for (int i = 0; i < playStoryModel.options[index].eventCheck.sucessResult.Length; i++)
            {
                GameSystem.instance.ApplyChoice(currentStoryModel.options[index].eventCheck.sucessResult[i]);
                CheckEventTypeNone = true;
            }
        }

        bool CheckValue = false;

    }



    public void StoryModelInit()                                //스토리 모델 Init 
    {
        fullText = currentStoryModel.storyText;

        storyIndex.text = currentStoryModel.storyNumber.ToString();

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWayText[i].text = currentStoryModel.options[i].buttonText;
        }
    }

    public void ResetShow()                                             //사용된 Commpont 초기화 
    {
        textComponent.text = "";

        for (int i = 0; i < buttonWay.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttonWay.Length; i++)
        {
            int wayIndex = i;       //클로저 (Closure) 문제를 해결 하기 위해서 
            //클로저 문제 -> 람다식 또는 익명 함수가 외부 변수를 캡쳐할 때 발생하는 문제 
            buttonWay[i].onClick.AddListener(() => OnWayClick(wayIndex));        //()=> OnWayClick(i) 로 썼을때는 2 값만 계속 들어감
        }

        CoShowText();
    }

    public void CoShowText()
    {
        StoryModelInit();
        ResetShow();
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()                                              //앞에 선언한 모든 컴포넌트들을 Model 과 함수를 통해서 진행
    {
        if (currentStoryModel.mainImage != null)
        {
            //Texture2D를 Sprite로 변환 
            Rect rect = new Rect(0, 0, currentStoryModel.mainImage.width, currentStoryModel.mainImage.height);   //모델의 높이와 너비
            Vector2 pivot = new Vector2(0.5f, 0.5f);    //스프라이트 축(중심) 지정
            Sprite sprite = Sprite.Create(currentStoryModel.mainImage, rect, pivot);

            //Sprite변환된 이미지를 컴포넌트에 넣는다.  
            imageComponent.sprite = sprite;
        }
        else
        {
            Debug.LogError("텍스쳐의 이상이 있다.");
        }

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i < currentStoryModel.options.Length; i++)
        {
            buttonWay[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delay);
    }


}