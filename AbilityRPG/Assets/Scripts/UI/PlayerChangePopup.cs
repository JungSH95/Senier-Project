using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerChangePopup : MonoBehaviour
{
    protected PlayerCharacterChange characterChange;
    protected PlayerController playerController;

    public GameObject popupWindow;
    public Button yesButton;
    public Button noButton;

    // 변경 할 캐릭터의 정보
    public RawImage characterImg;
    public TextMeshProUGUI name;
    public TextMeshProUGUI status;

    protected int characterNumber;

    public List<Sprite> characterSprite;

    public Joystick joystick;

    private void Start()
    {
        characterChange = this.gameObject.GetComponent<PlayerCharacterChange>();

        joystick = FindObjectOfType<Joystick>();

        yesButton.onClick.AddListener(YesClick);
        noButton.onClick.AddListener(NoClick);
    }

    public void OpenPopupWindows(string characterName)
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.isPopup = true;
        joystick.gameObject.SetActive(false);

        characterNumber = -1;

        switch (characterName)
        {
            case "토순":
                characterNumber = 0;
                status.text = "원거리 타입";
                break;
            case "펭수":
                characterNumber = 1;
                status.text = "원거리 타입";
                break;
            case "럭부":
                characterNumber = 2;
                status.text = "원거리 타입";
                break;
        }

        characterImg.texture = characterSprite[characterNumber].texture;
        name.text = characterName;

        popupWindow.SetActive(true);
    }

    void YesClick()
    {
        popupWindow.SetActive(false);
        joystick.gameObject.SetActive(true);

        characterChange.CharacterChange(characterNumber);
    }

    void NoClick()
    {
        popupWindow.SetActive(false);
        joystick.gameObject.SetActive(true);
        playerController.isPopup = false;
    }
}
