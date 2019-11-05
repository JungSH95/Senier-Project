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
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI characterStatus;

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

    public void OpenPopupWindows(string name)
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.isPopup = true;
        joystick.gameObject.SetActive(false);

        characterNumber = -1;

        switch (name)
        {
            case "토순":
                characterNumber = 0;
                characterStatus.text = "무기 : 당근 바구니";
                break;
            case "펭수":
                characterNumber = 1;
                characterStatus.text = "무기 : 폭탄";
                break;
            case "럭부":
                characterNumber = 2;
                characterStatus.text = "무기 : 럭비공";
                break;
        }

        characterImg.texture = characterSprite[characterNumber].texture;
        characterName.text = name;

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
