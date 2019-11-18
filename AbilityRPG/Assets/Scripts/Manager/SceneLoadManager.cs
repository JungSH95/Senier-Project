using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    public string nextScene;

    public FadeManager fadeManager;

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;

        if (GameManager.Instance != null)
            GameManager.Instance.currentScene = nextScene;

        StartCoroutine(CoLoadScene());
    }

    //로비 씬 로드
    public void LobbyScene()
    {
        SceneManager.LoadScene("0_Lobby");
    }

    public void WorldMapScene()
    {
        SceneManager.LoadScene("1_MainField");
    }

    public void BattleScene()
    {
        nextScene = "2_BattleField";

        if (GameManager.Instance != null)
            GameManager.Instance.currentScene = nextScene;

        StartCoroutine(CoFadeOut());
    }

    public void TutorialScene()
    {
        nextScene = "3_TutorialField";

        if (GameManager.Instance != null)
            GameManager.Instance.currentScene = nextScene;

        StartCoroutine(CoFadeOut());
    }

    IEnumerator CoLoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress >= 0.9f)
                op.allowSceneActivation = true;
        }
    }

    IEnumerator CoFadeOut()
    {
        fadeManager.FadeOut();
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CoLoadScene());
    }
}
