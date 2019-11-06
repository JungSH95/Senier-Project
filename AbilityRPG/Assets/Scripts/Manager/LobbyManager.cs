using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
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
    }
}
