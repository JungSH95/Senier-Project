using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public Transform playerPos;

    private FadeManager fadeManager;
    
    void Start()
    {
        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
    }

    public void CharacterInitButton()
    {
        StartCoroutine(CoCharacterInit());
    }

    IEnumerator CoCharacterInit()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneLoadManager._sceneManagerInstance.LoadScene("1_CharacterInit");
    }
}
