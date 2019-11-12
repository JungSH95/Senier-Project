using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI characterName;

    public Slider conSlider;
    public Slider strSlider;
    public Slider dexSlider;
    public Slider weaponSlider;

    public Text conLevel;
    public Text strLevel;
    public Text dexLevel;
    public Text weaponLevel;

    public new GameObject camera;

    [Header("Object")]
    public GameObject[] characterObjects;
    public GameObject[] weaponObjects;

    private int currentNumber;

    private float conMax;
    private float strMax;
    private float dexMax;
    private float weaponMax;

    public void OpenPlayerInfoUI(int number = 0)
    {
        currentNumber = number;

        switch (currentNumber)
        {
            case 0:
                characterName.text = "토순";
                break;
            case 1:
                characterName.text = "펭수";
                break;
            case 2:
                characterName.text = "럭부";
                break;
        }

        conMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].conLevel);
        strMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].strLevel);
        dexMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].dexLevel);
        weaponMax = Mathf.Pow(3, GameManager.Instance.characterInfoList[currentNumber].weaponLevel);

        conSlider.value = GameManager.Instance.characterInfoList[currentNumber].conExp / conMax;
        strSlider.value = GameManager.Instance.characterInfoList[currentNumber].strExp / strMax;
        dexSlider.value = GameManager.Instance.characterInfoList[currentNumber].dexExp / dexMax;
        weaponSlider.value = GameManager.Instance.characterInfoList[currentNumber].weaponExp / weaponMax;

        conLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].conLevel;
        strLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].strLevel;
        dexLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].dexLevel;
        weaponLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].weaponLevel;

        characterObjects[currentNumber].SetActive(true);
        weaponObjects[currentNumber].SetActive(true);

        camera.SetActive(true);
    }

    public void ClosePlayerInfoUI()
    {
        characterObjects[currentNumber].SetActive(false);
        weaponObjects[currentNumber].SetActive(false);

        camera.SetActive(false);
    }

    public void ConExpUpClick()
    {
        GameManager.Instance.characterInfoList[currentNumber].conExp += 1;

        if (GameManager.Instance.characterInfoList[currentNumber].conExp >= conMax)
        {
            GameManager.Instance.characterInfoList[currentNumber].ConLevelUp();

            conMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].conLevel);
            conLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].conLevel;
        }

        conSlider.value = GameManager.Instance.characterInfoList[currentNumber].conExp / conMax;
    }

    public void StrExpUpClick()
    {
        GameManager.Instance.characterInfoList[currentNumber].strExp += 1;

        if (GameManager.Instance.characterInfoList[currentNumber].strExp >= strMax)
        {
            GameManager.Instance.characterInfoList[currentNumber].StrLevelUp();

            strMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].strLevel);
            strLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].strLevel;
        }

        strSlider.value = GameManager.Instance.characterInfoList[currentNumber].strExp / strMax;
    }

    public void DexExpUpClick()
    {
        // 만렙일 경우
        if (GameManager.Instance.characterInfoList[currentNumber].dexLevel ==
            GameManager.Instance.characterInfoList[currentNumber].dexMaxLevel)
            return;

        GameManager.Instance.characterInfoList[currentNumber].dexExp += 1;

        if (GameManager.Instance.characterInfoList[currentNumber].dexExp >= dexMax)
        {
            GameManager.Instance.characterInfoList[currentNumber].DexLevelUp();

            dexMax = Mathf.Pow(2, GameManager.Instance.characterInfoList[currentNumber].dexLevel);

            if (GameManager.Instance.characterInfoList[currentNumber].dexLevel !=
            GameManager.Instance.characterInfoList[currentNumber].dexMaxLevel)
                dexLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].dexLevel;
            else
                dexLevel.text = "LV.MAX";
        }

        dexSlider.value = GameManager.Instance.characterInfoList[currentNumber].dexExp / dexMax;
    }

    public void WeaponExpUpClick()
    {
        GameManager.Instance.characterInfoList[currentNumber].weaponExp += 1;

        if (GameManager.Instance.characterInfoList[currentNumber].weaponExp >= weaponMax)
        {
            GameManager.Instance.characterInfoList[currentNumber].WeaponLevelUp();

            weaponMax = Mathf.Pow(3, GameManager.Instance.characterInfoList[currentNumber].weaponLevel);
            weaponLevel.text = "LV." + GameManager.Instance.characterInfoList[currentNumber].weaponLevel;
        }

        weaponSlider.value = GameManager.Instance.characterInfoList[currentNumber].weaponExp / weaponMax;
    }
}
