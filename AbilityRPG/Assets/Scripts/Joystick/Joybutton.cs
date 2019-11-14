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
    }

    public void CharacterBookButton()
    {
        MainFieldManager.Instance.CharacterBookOpen();
    }

    public void OptionButton()
    {
        MainFieldManager.Instance.OptionOpen();
    }
    // -----------------------------------------------------

    public void BattleExitButton()
    {
        FieldManager.Instance.exitPanel.ExitPanelOpen();
    }
}