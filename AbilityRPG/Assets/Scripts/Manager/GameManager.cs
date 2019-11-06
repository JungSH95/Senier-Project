using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public PlayerData playerData;
    public OptionData optionData;

    public List<PlayerBase> characterInfoList;

    private bool isPaused;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        JsonManager.Instance.PlayerDataLoad();
    }

    private void Update()
    {
        // 뒤로가기 버튼
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
    }


    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            isPaused = true;
            Debug.Log("일시정지");
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

    private void OnApplicationQuit()
    {
        Debug.Log("게임 종료");

        JsonManager.Instance.PlayerDataSave();
    }
}
