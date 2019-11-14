using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleExitPanel : MonoBehaviour
{
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
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void YesButtonClick()
    {
        Debug.Log("YesButton 클릭");

        // 캐릭터 사망처리 후
        // 결과창 표시
        FieldManager.Instance.player.GetComponent<PlayerController>().playerHpBar.Dmg(999);
        FieldManager.Instance.player.GetComponent<PlayerController>().PlayerDead();
        this.gameObject.SetActive(false);
        Time.timeScale = 0.5f;
    }

    public void NoButtonClick()
    {
        Debug.Log("NoButton 클릭");

        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
