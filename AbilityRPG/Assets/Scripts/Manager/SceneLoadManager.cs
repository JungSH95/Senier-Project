using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager _sceneManagerInstance;
    public string nextScene;

    private void Awake()
    {
        //싱글톤 화
        if (_sceneManagerInstance == null)
        {
            _sceneManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(CoLoadScene());
    }

    //로비 씬 로드
    public void LobbyScene()
    {
        SceneManager.LoadScene("0_Lobby");
    }
    //캐릭터 생성 씬 로드
    public void CharacterInitScene()
    {
        SceneManager.LoadScene("1_CharacterInit");
    }
    //월드맵 로드
    public void WorldMapScene()
    {
        SceneManager.LoadScene("2_MainField");
    }
    //배틀 씬 로드
    public void BattleScene()
    {
        SceneManager.LoadScene("3_BattleField");
    }

    IEnumerator CoLoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;

            Debug.Log(op.progress);
            if (op.progress >= 0.9f)
                op.allowSceneActivation = true;
        }

        Debug.Log("로딩 끝");
    }
}
