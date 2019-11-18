using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleExitPanel : MonoBehaviour
{
    public PlayerController player;

    void Start()
    {
        SetExitPanel();
    }

    public void SetExitPanel()
    {
        this.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(YesButtonClick);
        this.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(NoButtonClick);
    }

    public void ExitPanelOpen()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void YesButtonClick()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);

        player.playerHpBar.Dmg(999);
        player.PlayerDead();

        this.gameObject.SetActive(false);
        Time.timeScale = 0.5f;
    }

    public void NoButtonClick()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);

        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
