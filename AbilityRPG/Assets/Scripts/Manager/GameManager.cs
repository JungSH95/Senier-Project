using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerData playerData;
    public OptionData optionData;

    public List<CharacterBase> characterInfoList;

    private bool isPaused;

    public string currentScene;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 40;

        currentScene = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        JsonManager.Instance.PlayerDataLoad();
        JsonManager.Instance.OptionDataLoad();
        
        for (int i = 0; i < 3; i++)
            JsonManager.Instance.CharacterDataLoad(i);
    }

    // 화면 내렸을때
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            isPaused = true;

            if(currentScene == "2_BattleField")
                FieldManager.Instance.exitPanel.ExitPanelOpen();
        }
        else
        {
            if(isPaused)
            {
                isPaused = false;
            }
        }
    }

    // 어플리케이션 종료 시 호출
    private void OnApplicationQuit()
    {
        JsonManager.Instance.PlayerDataSave();
        JsonManager.Instance.OptionDataSave();

        for (int i = 0; i < 3; i++)
            JsonManager.Instance.CharacterDataSave(i);
    }
}
