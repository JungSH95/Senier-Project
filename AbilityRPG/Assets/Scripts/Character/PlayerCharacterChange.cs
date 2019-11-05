﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterChange : MonoBehaviour
{
    public List<GameObject> characterList;

    // 현재 위치
    public GameObject nowPlayer;

    public FadeManager fadeManager;

    public Joystick joystick;

    private void Awake()
    {
        fadeManager = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
        nowPlayer = GameObject.FindGameObjectWithTag("Player");

        joystick = FindObjectOfType<Joystick>();
    }

    public void CharacterChange(int number)
    {
        if (characterList.Count == 0 || number < 0 || characterList.Count <= number)
            return;

        StartCoroutine(CoCharacterChange(number));
    }

    IEnumerator CoCharacterChange(int number)
    {
        fadeManager.FadeOut();

        yield return new WaitForSeconds(0.5f);

        nowPlayer.SetActive(false);

        joystick.gameObject.SetActive(true);
        GameObject newPlayer = Instantiate(characterList[number]);
        newPlayer.GetComponent<PlayerController>().PlayerSetting();
        Camera.main.GetComponent<CameraMovement>().Player = newPlayer;
        nowPlayer = newPlayer;

        NPCManager.Instance.CharacterChange(JsonManager.Instance.playerData.characterNumber, number);
        JsonManager.Instance.playerData.characterNumber = number;

        fadeManager.FadeIn();

        yield return null;
    }
}
