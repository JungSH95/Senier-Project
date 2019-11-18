using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    public GameObject nowField;
    private GameObject portal;

    // base battle 방 시작 위치
    public List<Transform> startPosList;

    public List<GameObject> characterList;

    public Transform playerPos;
    public GameObject player;

    public int currentStage;
    public int lastStage;

    public bool isBattle;
    public bool isClear;

    private FadeManager fadeManager;
    public StageTextAnimation stageTextAnimation;

    [Header("UI")]
    public BattleExitPanel exitPanel;

    private void Awake()
    {
        currentStage = 0;
        lastStage = 3;

        isClear = false;

        // 테스트 위해서 (임시 방편으로 만들긴 했는데 되긴 함)
        if (GameManager.Instance == null)
        {
            player = Instantiate(characterList[0]);
            player.transform.parent = playerPos.transform;
        }
        else
        {
            player = Instantiate(characterList[GameManager.Instance.playerData.characterNumber]);
            player.transform.parent = playerPos.transform;
        }

        fadeManager = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
        exitPanel.player = player.GetComponent<PlayerController>();
    }

    private void Start()
    {
        stageTextAnimation.StageAnimationStart("튜 토 리 얼");

        portal = nowField.transform.Find("Portal").gameObject;
        portal.transform.GetChild(0).GetComponent<Animator>().SetBool("Clear", false);
        portal.SetActive(false);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKey(KeyCode.Escape))
                exitPanel.ExitPanelOpen();
    }

    public void NextStage()
    {
        currentStage++;

        // 마지막 스테이지 클리어 시
        if (currentStage > lastStage)
            return;

        StartCoroutine(CoNextStage());
    }

    IEnumerator CoNextStage()
    {
        fadeManager.FadeOut();

        nowField.SetActive(false);
        player.SetActive(false);    // 조이스틱 이동과 중복 되서 위치 이동 이상하게 됌 그래서 추가
        player.GetComponent<PlayerTargeting>().monsterList = null;

        yield return new WaitForSeconds(0.5f);      // 시각적으로 보이는 것 때문에 일부로 딜레이

        nowField = startPosList[currentStage - 1].parent.gameObject;
        player.transform.position = startPosList[currentStage - 1].position;

        nowField.SetActive(true);
        isBattle = false;

        // 포탈 Obj 초기화
        portal = nowField.transform.Find("Portal").gameObject;
        portal.transform.GetChild(0).GetComponent<Animator>().SetBool("Clear", false);
        portal.SetActive(false);

        player.SetActive(true);
        player.GetComponent<PlayerController>().animator.SetFloat("AtkSpeed",
            player.GetComponent<PlayerController>().characterBase.atkSpeed);

        fadeManager.FadeIn();
        stageTextAnimation.StageAnimationStart(currentStage.ToString() + " - 튜토리얼");
    }

    public void StageStart()
    {
        if (isBattle)
            return;

        switch (currentStage)
        {
            case 0:
                StartCoroutine(CoFaseOne());
                break;
            case 1:
                StartCoroutine(CoFaseTwo());
                break;
        }
    }

    IEnumerator CoFaseOne()
    {
        yield return new WaitForSeconds(0.7f);

        portal.SetActive(true);
        portal.transform.GetChild(0).GetComponent<Animator>().SetBool("Clear", true);
    }

    IEnumerator CoFaseTwo()
    {
        isBattle = true;

        // NPC 대사 대기
        yield return new WaitForSeconds(4f);

        for (int i = 3; i > 0; i--)
        {
            stageTextAnimation.StageAnimationStart(i.ToString());
            yield return new WaitForSeconds(1f);
        }

        stageTextAnimation.StageAnimationStart("전투 시작");

        player.GetComponent<PlayerController>().characterBase.damage = 50;

        // 몬스터 생성
        SpawnManager.Instance.SetSpawnTransform(nowField);
        SpawnManager.Instance.MonsterAllSetActive();
        SpawnManager.Instance.MonsterAIStart();

        player.GetComponent<PlayerTargeting>().monsterList = SpawnManager.Instance.monsterList;
    }

    public void TutorialFieldEnd(bool clear)
    {
        isClear = clear;

        // 공격력 원래대로
        player.GetComponent<PlayerController>().characterBase.SetStatus();
        StartCoroutine(CoResult());
    }

    IEnumerator CoResult()
    {
        yield return new WaitForSeconds(1f);

        if (isClear)
        {
            stageTextAnimation.StageAnimationStart("완 료");
            GameManager.Instance.playerData.tutorialClear = true;
        }
        else
            stageTextAnimation.StageAnimationStart("실 패");

        yield return new WaitForSeconds(1f);
        fadeManager.FadeOut();
        yield return new WaitForSeconds(1f);
        SceneLoadManager.Instance.LoadScene("1_MainField");
    }
}
