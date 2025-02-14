﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFieldManager : Singleton<MainFieldManager>
{
    public List<GameObject> characterList;
    public GameObject player;

    [Header("PlayerInfo Open/Close")]
    public GameObject joystickCanvas;
    public GameObject mainCamera;

    [Header("PopupWindows")]
    public GameObject playerInfo;
    public GameObject characterBook;
    public GameObject option;
    public GameObject gameEnd;
    public StageTextAnimation textAnimation;

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

    private void Start()
    {
        Time.timeScale = 1f;
        textAnimation.StageAnimationStart("마 을");
    }

    private void Update()
    {
        // 뒤로가기 버튼
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKey(KeyCode.Escape))
                GameEndOpen();

        if (Input.GetKey(KeyCode.Q))
            GameEndOpen();
    }

    public void PlayerInfoOpen(int number)
    {
        if (joystickCanvas == null || mainCamera == null || playerInfo == null)
            return;

        player.GetComponent<PlayerController>().isPopup = true;
        //player.SetActive(false);

        joystickCanvas.SetActive(false);
        mainCamera.SetActive(false);

        playerInfo.SetActive(true);
        playerInfo.GetComponent<PlayerInfoUI>().OpenPlayerInfoUI(number);
    }

    public void PlayerInfoClose()
    {
        if (joystickCanvas == null || mainCamera == null || playerInfo == null)
            return;

        //player.SetActive(true);
        player.GetComponent<PlayerController>().isPopup = false;

        joystickCanvas.SetActive(true);
        mainCamera.SetActive(true);
        
        playerInfo.GetComponent<PlayerInfoUI>().ClosePlayerInfoUI();
        playerInfo.SetActive(false);
    }

    public void CharacterBookOpen()
    {
        characterBook.SetActive(true);
        player.GetComponent<PlayerController>().isPopup = true;
    }

    public void CharacterBookClose()
    {
        characterBook.SetActive(false);
        player.GetComponent<PlayerController>().isPopup = false;
    }

    public void OptionOpen()
    {
        option.SetActive(true);
        player.GetComponent<PlayerController>().isPopup = true;
    }

    public void OptionClose()
    {
        option.SetActive(false);
        player.GetComponent<PlayerController>().isPopup = false;
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }

    public void GameEndOpen()
    {
        gameEnd.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(Application.Quit);
        player.GetComponent<PlayerController>().isPopup = true;
        gameEnd.SetActive(true);
    }

    public void GameEndClose()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
        player.GetComponent<PlayerController>().isPopup = false;
        gameEnd.SetActive(false);
    }
}
