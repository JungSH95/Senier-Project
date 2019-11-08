using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using System.Text;

// https://wergia.tistory.com/164

[System.Serializable]
public class PlayerData
{
    // 사용 중 캐릭터 번호
    public int characterNumber;             

    // 사용이 가능한 캐릭터인지
    public bool[] characterUsed;

    public int resourceExp;

    public PlayerData()
    {
        characterNumber = 0;
        resourceExp = 0;
        characterUsed = new bool[] { true, false, true };
    }

    public void Print()
    {
        Debug.Log("Number = " + characterNumber);

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(string.Format("characterUsed[{0}] = {1}", i, characterUsed[i]));
        }
    }
}

[System.Serializable]
public class OptionData
{
    public bool BgmOn;
    public bool EfxOn;

    public OptionData()
    {
        BgmOn = true;
        EfxOn = true;
    }
}

public class JsonManager : Singleton<JsonManager>
{   

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void PlayerDataLoad()
    {
        if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            if(File.Exists(Application.dataPath + "/PlayerData.json"))
                GameManager.Instance.playerData = LoadJsonFile<PlayerData>(Application.dataPath, "PlayerData");
            else
            {
                GameManager.Instance.playerData = new PlayerData();
                string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
                CreateJsonFile(Application.dataPath, "PlayerData", jsonData);
            }
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Application.persistentDataPath + "/PlayerData.json"))
                GameManager.Instance.playerData = LoadJsonFile<PlayerData>(Application.persistentDataPath, "PlayerData");
            else
            {
                GameManager.Instance.playerData = new PlayerData();
                string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
                CreateJsonFile(Application.persistentDataPath, "PlayerData", jsonData);
            }
        }
    }

    public void PlayerDataSave()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
            CreateJsonFile(Application.dataPath, "PlayerData", jsonData);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
            CreateJsonFile(Application.persistentDataPath, "PlayerData", jsonData);
        }
    }

    public void OptionDataLoad()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (File.Exists(Application.dataPath + "/OptionData.json"))
                GameManager.Instance.optionData = LoadJsonFile<OptionData>(Application.dataPath, "OptionData");
            else
            {
                GameManager.Instance.optionData = new OptionData();
                string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
                CreateJsonFile(Application.dataPath, "OptionData", jsonData);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Application.persistentDataPath + "/OptionData.json"))
                GameManager.Instance.optionData = LoadJsonFile<OptionData>(Application.persistentDataPath, "OptionData");
            else
            {
                GameManager.Instance.optionData = new OptionData();
                string jsonData = JsonUtility.ToJson(GameManager.Instance.playerData);
                CreateJsonFile(Application.persistentDataPath, "OptionData", jsonData);
            }
        }
    }

    public void OptionDataSave()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.optionData);
            CreateJsonFile(Application.dataPath, "OptionData", jsonData);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.optionData);
            CreateJsonFile(Application.persistentDataPath, "OptionData", jsonData);
        }
    }

    public void CharacterDataLoad(int number)
    {
        string fileName = "Character" + number.ToString();
        string weapon = "Weapon" + number.ToString();
                       
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (File.Exists(Application.dataPath + "/" + fileName + ".json"))
                GameManager.Instance.characterInfoList.Add(LoadJsonFile<CharacterBase>(Application.dataPath, fileName));
            else
            {
                GameManager.Instance.characterInfoList.Add(new CharacterBase(weapon));
                string jsonData = JsonUtility.ToJson(GameManager.Instance.characterInfoList[number]);
                CreateJsonFile(Application.dataPath, fileName, jsonData);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Application.persistentDataPath + "/" + fileName + ".json"))
                GameManager.Instance.characterInfoList.Add(LoadJsonFile<CharacterBase>(Application.persistentDataPath, fileName));
            else
            {
                GameManager.Instance.characterInfoList.Add(new CharacterBase(weapon));
                string jsonData = JsonUtility.ToJson(GameManager.Instance.characterInfoList[number]);
                CreateJsonFile(Application.persistentDataPath, fileName, jsonData);
            }
        }

    }

    public void CharacterDataSave(int number)
    {
        string fileName = "Character" + number.ToString();

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.characterInfoList[number]);
            CreateJsonFile(Application.dataPath, fileName, jsonData);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string jsonData = JsonUtility.ToJson(GameManager.Instance.characterInfoList[number]);
            CreateJsonFile(Application.persistentDataPath, fileName, jsonData);
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
