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

        conSlider.value = 0f;
        strSlider.value = 0f;
        dexSlider.value = 0f;
        weaponSlider.value = 0f;

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
        Debug.Log("건강 렙업");

        GameManager.Instance.characterInfoList[currentNumber].conExp += 1;
        Debug.Log("건강 경험치 : " + GameManager.Instance.characterInfoList[currentNumber].conExp);
    }

    public void StrExpUpClick()
    {
        Debug.Log("근력 렙업");

        GameManager.Instance.characterInfoList[currentNumber].strExp += 1;
        Debug.Log("근력 경험치 : " + GameManager.Instance.characterInfoList[currentNumber].strExp);
    }

    public void DexExpUpClick()
    {
        Debug.Log("민첩 렙업");

        GameManager.Instance.characterInfoList[currentNumber].dexExp += 1;
        Debug.Log("민첩 경험치 : " + GameManager.Instance.characterInfoList[currentNumber].dexExp);
    }

    public void WeaponExpUpClick()
    {
        Debug.Log("무기 렙업");

        GameManager.Instance.characterInfoList[currentNumber].weaponExp += 1;
        Debug.Log("무기 경험치 : " + GameManager.Instance.characterInfoList[currentNumber].weaponExp);
    }
}
