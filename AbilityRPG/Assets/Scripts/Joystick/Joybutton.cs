using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Joybutton : MonoBehaviour
{
    public List<Sprite> characterSprite;

    public Image playerInfoImage;

    private void Start()
    {
        if (GameManager.Instance != null && SceneManager.GetActiveScene().name == "1_MainField")
            playerInfoImage.sprite = characterSprite[GameManager.Instance.playerData.characterNumber];
    }

    // MainField -------------------------------------------
    public void SetPlayerInfoImage()
    {
        playerInfoImage.sprite = characterSprite[GameManager.Instance.playerData.characterNumber];
    }

    public void PlayerInfoButton()
    {
        MainFieldManager.Instance.PlayerInfoOpen(GameManager.Instance.playerData.characterNumber);
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }

    public void CharacterBookButton()
    {
        MainFieldManager.Instance.CharacterBookOpen();
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }

    public void OptionButton()
    {
        MainFieldManager.Instance.OptionOpen();
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }
    // -----------------------------------------------------

    public void BattleExitButton()
    {
        FieldManager.Instance.exitPanel.ExitPanelOpen();
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
    }
}