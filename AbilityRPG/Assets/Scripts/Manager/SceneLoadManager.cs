using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public string nextScene;

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

    //월드맵 로드
    public void WorldMapScene()
    {
        SceneManager.LoadScene("1_MainField");
    }
    //배틀 씬 로드
    public void BattleScene()
    {
        SceneManager.LoadScene("2_BattleField");
    }

    IEnumerator CoLoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;

            if (op.progress >= 0.9f)
                op.allowSceneActivation = true;
        }
    }
}
