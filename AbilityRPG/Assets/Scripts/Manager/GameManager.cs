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

    private void Awake()
    {
        DontDestroyOnLoad(this);
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
            Debug.Log("일시정지");

            if(SceneManager.GetActiveScene().name == "2_BattleField")
                FieldManager.Instance.exitPanel.ExitPanelOpen();
        }
        else
        {
            if(isPaused)
            {
                isPaused = false;
                Debug.Log("일시정지 상태에서 돌아옴");
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
