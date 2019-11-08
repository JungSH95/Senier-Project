using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // 어플리케이션 종료 시 호출
    private void OnApplicationQuit()
    {
        JsonManager.Instance.PlayerDataSave();
        JsonManager.Instance.OptionDataSave();

        //for (int i = 0; i < characterPrefebs.Count; i++)
            //JsonManager.Instance.CharacterDataSave(i);
    }
}
