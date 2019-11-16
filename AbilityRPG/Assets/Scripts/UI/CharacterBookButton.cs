using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBookButton : MonoBehaviour
{
    public Image characterImage;
    public Sprite characterSprite;

    public int buttonNumber;

    void Start()
    {
        if (GameManager.Instance != null)
            if (GameManager.Instance.playerData.characterUsed[buttonNumber])
            {
                characterImage.sprite = characterSprite;
                this.gameObject.GetComponent<Button>().onClick.AddListener(ButtonClick);
            }
    }

    void ButtonClick()
    {
        MainFieldManager.Instance.PlayerInfoOpen(buttonNumber);
        this.gameObject.transform.parent.gameObject.SetActive(false);
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }
}
