using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public Transform playerPos;

    private FadeManager fadeManager;
    
    void Start()
    {
        fadeManager = GameObject.Find("FadeCanvas").GetComponent<FadeManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("게임 시작");
            SceneLoadManager.Instance.WorldMapScene();
        }

        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
    }
}
