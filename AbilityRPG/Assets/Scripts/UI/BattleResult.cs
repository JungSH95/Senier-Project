using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleResult : MonoBehaviour
{
    private Animator animator;

    [Header("UI")]
    public Image characterImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stageCountText;
    public TextMeshProUGUI mosterCountText;
    public TextMeshProUGUI expCountText;
    public TextMeshProUGUI resultText;

    public Button backButton;

    public List<Sprite> characterSprites;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetResultUI(bool clear)
    {
        int characterNumber = 0;

        if (GameManager.Instance != null)
            characterNumber = GameManager.Instance.playerData.characterNumber;

        switch (characterNumber)
        {
            case 0:
                nameText.text = "토순";
                break;
            case 1:
                nameText.text = "펭수";
                break;
            case 2:
                nameText.text = "럭부";
                break;
        }

        characterImage.sprite = characterSprites[characterNumber];
        stageCountText.text = FieldManager.Instance.currentStage.ToString();
        mosterCountText.text = FieldManager.Instance.monsterDeadCount.ToString();
        expCountText.text = FieldManager.Instance.expCount.ToString();

        if (clear)
            resultText.text = "클 리 어";
        else
            resultText.text = "사 망";
    }

    public IEnumerator CoResultOpen()
    {
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("Open", true);

        if (SoundManager.Instance.backgroundAudio.volume != 0f)
            SoundManager.Instance.backgroundAudio.volume = 0.4f;

        // 플레이어 데이터 저장 (expCount)
        GameManager.Instance.playerData.resourceExp += FieldManager.Instance.expCount;

    }

    public void BackButtonClick()
    {
        SceneLoadManager.Instance.LoadScene("1_MainField");
        backButton.enabled = false;
    }
}
