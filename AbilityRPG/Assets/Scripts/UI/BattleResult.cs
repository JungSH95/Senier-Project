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

    public Image hiddenCharacterImage;

    public Button backButton;

    public List<Sprite> characterSprites;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetResultUI()
    {
        int characterNumber = 0;

        if (GameManager.Instance != null)
        {
            characterNumber = GameManager.Instance.playerData.characterNumber;

            if (GameManager.Instance.playerData.characterUsed[FieldManager.Instance.hiddenStageType + 1] == false)
                if (FieldManager.Instance.hiddenStageClear)
                    hiddenCharacterImage.sprite = characterSprites[FieldManager.Instance.hiddenStageType + 1];
        }

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

        if (FieldManager.Instance.isClear)
            resultText.text = "클 리 어";
        else
            resultText.text = "사 망";
    }

    public IEnumerator CoResultOpen()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;

        animator.SetBool("Open", true);

        if (SoundManager.Instance.backgroundAudio.volume != 0f)
            SoundManager.Instance.backgroundAudio.volume = 0.4f;

        if (GameManager.Instance != null)
        {
            // 플레이어 데이터 저장 (expCount)
            GameManager.Instance.playerData.resourceExp += FieldManager.Instance.expCount;

            // 히든 스테이지 클리어 시 캐릭터 해금
            if (FieldManager.Instance.hiddenStageClear &&
                GameManager.Instance.playerData.characterUsed[FieldManager.Instance.hiddenStageType + 1] == false)
            {
                GameManager.Instance.playerData.characterUsed[FieldManager.Instance.hiddenStageType + 1] = true;
                animator.SetBool("HiddenClear", true);
            }
        }
    }

    public void BackButtonClick()
    {
        SoundManager.Instance.effectAudio.PlayOneShot(SoundManager.Instance.UiEFXSounds[0]);

        SceneLoadManager.Instance.LoadScene("1_MainField");
        backButton.enabled = false;
    }
}
