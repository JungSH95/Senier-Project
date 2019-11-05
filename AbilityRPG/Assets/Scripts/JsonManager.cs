using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;
using UnityEngine.UI;

[System.Serializable]
public class PlayerData
{
    // 사용 중 캐릭터 번호
    public int characterNumber;             

    // 사용이 가능한 캐릭터인지
    public bool character0;
    public bool character1;
    public bool character2;

    public PlayerData() { }

    public PlayerData(bool isTest)
    {
        if(isTest)
        {
            characterNumber = 0;

            character0 = true;
            character1 = true;
            character2 = true;
        }
    }

    public void Print()
    {
        Debug.Log("Number = " + characterNumber);

        Debug.Log("character0 = " + character0);
        Debug.Log("character1 = " + character1);
        Debug.Log("character2 = " + character2);
    }
}

[System.Serializable]
public class OptionData
{
    public bool BgmOn;
    public bool EfxOn;
}

public class JsonManager : Singleton<JsonManager>
{
    public PlayerData playerData;
    public OptionData optionData;

    private void Start()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if(File.Exists(Application.dataPath + "/PlayerData.json"))
                playerData = LoadJsonFile<PlayerData>(Application.dataPath, "PlayerData");
            else
            {
                playerData = new PlayerData(true);
                string jsonData = JsonUtility.ToJson(playerData);
                CreateJsonFile(Application.dataPath, "PlayerData", jsonData);
            }
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
                playerData = LoadJsonFile<PlayerData>(Application.persistentDataPath, "PlayerData");
            else
            {
                playerData = new PlayerData(true);
                string jsonData = JsonUtility.ToJson(playerData);
                CreateJsonFile(Application.persistentDataPath, "PlayerData", jsonData);
            }
        }
    }

    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath,
            fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath,
            fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);

        return JsonUtility.FromJson<T>(jsonData);
    }
}
