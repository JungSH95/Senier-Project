using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    bool nextScene;

    private void Awake()
    {
        nextScene = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!nextScene)
            {
                SceneLoadManager.Instance.LoadScene("1_MainField");
                nextScene = true;
            }
        }
    }
}
