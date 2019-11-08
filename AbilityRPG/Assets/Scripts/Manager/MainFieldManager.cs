using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldManager : Singleton<MainFieldManager>
{
    public List<GameObject> characterList;
    public GameObject player;

    [Header("PlayerInfo Open/Close")]
    public GameObject joystickCanvas;
    public GameObject mainCamera;
    public GameObject playerInfo;

    private void Awake()
    {
        // 테스트 위해서 (임시 방편으로 만들긴 했는데 되긴 함)
        if (GameManager.Instance == null)
        {
            player = Instantiate(characterList[0]);
        }
        else
        {
            player = Instantiate(characterList[GameManager.Instance.playerData.characterNumber]);
            player.GetComponent<PlayerController>().characterBase =
                GameManager.Instance.characterInfoList[GameManager.Instance.playerData.characterNumber];
        }
    }

    public void PlayerInfoOpen()
    {
        if (joystickCanvas == null || mainCamera == null || playerInfo == null)
            return;

        player.SetActive(false);

        joystickCanvas.SetActive(false);
        mainCamera.SetActive(false);
        playerInfo.SetActive(true);
    }

    public void PlayerInfoClose()
    {
        if (joystickCanvas == null || mainCamera == null || playerInfo == null)
            return;

        player.SetActive(true);

        joystickCanvas.SetActive(true);
        mainCamera.SetActive(true);
        playerInfo.SetActive(false);
    }
}
