using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;            //������ �а� ���� ���ؼ�
using System.Xml.Serialization;       //XML�� ����ϱ� ���ؼ�

[System.Serializable]
public class PlayerData             //������ ������ ����
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
        XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData));   //XML ����ȭ
        FileStream stream = new FileStream(filePath, FileMode.Create);  //���� ���� ��� ����
        serialzer.Serialize(stream, data);                              //���Ͽ� �����͸� �����Ѵ�.
        stream.Close();                                          //�ݵ�� Close ������Ѵ�.
    }

    PlayerData LoadData()
    {
        if(File.Exists(filePath))
        {
            XmlSerializer serialzer = new XmlSerializer(typeof(PlayerData));
            FileStream stream = new FileStream(filePath, FileMode.Open);            //���� �б� ���� ����
            PlayerData data = (PlayerData)serialzer.Deserialize(stream);            //����ȭ�Ȱ��� Ŭ������ ����
            stream.Close();                                                         //������ �ݴ´�.
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
            PlayerData playerData = new PlayerData();       //�÷��̾� ������ �����Ͽ�
            playerData.playerName = "�÷��̾� 1";           //�����͸� �����ش�.
            playerData.playerLevel = 1;
            playerData.items.Add("��1");
            playerData.items.Add("����1");
            SaveData(playerData);                           //�ش� ������ XML ���Ϸ� �����Ѵ�.
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();       //���� �÷��̾� ������ ��ü

            playerData = LoadData();                        //���Ͽ��� �ε��Ѵ�.

            Debug.Log(playerData.playerName);               //�ε�� �����͸� ����Ѵ�.
            for(int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }
}