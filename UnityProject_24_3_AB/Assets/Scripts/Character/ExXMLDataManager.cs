using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;            //파일을 읽고 쓰기 위해서
using System.Xml.Serialization;       //XML을 사용하기 위해서

[System.Serializable]
public class PlayerData             //저장할 데이터 선언
{
    public string playerName;
    public int playerLevel;
    public List<string> items = new List<string>();
}


public class ExXMLDataManager : MonoBehaviour
{
    string filePath;


    private void Start()
    {
        filePath = Application.persistentDataPath + "/playerData.xml";
        Debug.Log(filePath);
    }
    // Update is called once per frame

    void SaveData(PlayerData data)
    {
        XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData));   //XML 직렬화
        FileStream stream = new FileStream(filePath, FileMode.Create);  //파일 생성 모드 설정
        serialzer.Serialize(stream, data);                              //파일에 데이터를 저장한다.
        stream.Close();                                          //반드시 Close 해줘야한다.
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData));
            FileStream stream = new FileStream(filePath, FileMode.Open);            //파일 읽기 모드로 설정
            PlayerData data = (PlayerData)serialzer.Deserialize(stream);            //직열화된것을 클래스로 변경
            stream.Close();                                                         //파일을 닫는다.
            return data;
        }
        else
        {
            return null;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();       //플레이어 데이터 생성하여
            playerData.playerName = "플레이어 1";           //데이터를 정해준다.
            playerData.playerLevel = 1;
            playerData.items.Add("돌1");
            playerData.items.Add("바위1");
            SaveData(playerData);                           //해당 내용을 XML 파일로 저장한다.
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();       //받을 플레이어 데이터 객체

            playerData = LoadData();                        //파일에서 로딩한다.

            Debug.Log(playerData.playerName);               //로드된 데이터를 출력한다.
            for(int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }
}