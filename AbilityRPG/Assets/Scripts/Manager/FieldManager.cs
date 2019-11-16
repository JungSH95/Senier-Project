using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필드 관리
public class FieldManager : Singleton<FieldManager>
{
    public GameObject nowField;
    private GameObject portal;

    // base battle 방 시작 위치
    public List<Transform> startPosList;
    // 캐릭터 얻을 수 있는 비밀 방
    public List<Transform> hiddenStartPosList;

    public List<GameObject> characterList;

    public Transform playerPos;
    public GameObject player;

    public int currentStage;
    public int lastStage;

    public int monsterDeadCount;
    public int expCount;

    public bool isClear;

    private FadeManager fadeManager;
    public StageTextAnimation stageTextAnimation;

    [Header("UI")]
    public BattleExitPanel exitPanel;
    public BattleResult battleResult;

    private void Awake()
    {
        currentStage = 0;
        lastStage = 6;

        monsterDeadCount = 0;
        expCount = 0;

        // 테스트 위해서 (임시 방편으로 만들긴 했는데 되긴 함)
        if(GameManager.Instance == null)
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
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKey(KeyCode.Escape))
                exitPanel.ExitPanelOpen();
    }

    public void NextField()
    {
        currentStage++;

        // 마지막 스테이지 클리어 시
        if (currentStage > lastStage)
            return;
        
        StartCoroutine(CoNextField());
    }

    IEnumerator CoNextField()
    {
        fadeManager.FadeOut();

        SpawnManager.Instance.PointsMonsterCheck();
        while (SpawnManager.Instance.isMonsterClear == false)
            yield return new WaitForSeconds(0.01f);

        nowField.SetActive(false);
        player.SetActive(false);    // 조이스틱 이동과 중복 되서 위치 이동 이상하게 됌 그래서 추가
        player.GetComponent<PlayerTargeting>().monsterList = null;

        yield return new WaitForSeconds(0.5f);      // 시각적으로 보이는 것 때문에 일부로 딜레이


        if (currentStage == 5)
        {
            int randomIndex = Random.Range(0, 10);

            // 30퍼 (럭비 맵) 
            if (randomIndex < 3)
            {
                nowField = hiddenStartPosList[0].parent.gameObject;
                player.transform.position = hiddenStartPosList[0].position;
            }
            // 70퍼 (펭수 맵)
            else if (randomIndex < 10)
            {
                nowField = hiddenStartPosList[1].parent.gameObject;
                player.transform.position = hiddenStartPosList[1].position;
            }
        }
        else if(currentStage == 10)
        {
            // 보스 방
        }
        else
        {
            int randomFieldIndex = Random.Range(0, startPosList.Count);
            nowField = startPosList[randomFieldIndex].parent.gameObject;
            player.transform.position = startPosList[randomFieldIndex].position;
        }
        
        nowField.SetActive(true);

        // 포탈 Obj 초기화
        portal = nowField.transform.Find("Portal").gameObject;
        portal.transform.GetChild(0).GetComponent<Animator>().SetBool("Clear", false);
        portal.SetActive(false);

        // 스폰 위치 설정 및 스폰 시작
        SpawnManager.Instance.SetSpawnTransform(nowField);

        // 몬스터 생성이 끝날때까지 대기
        while (SpawnManager.Instance.isSpawnEnd != true)
            yield return new WaitForSeconds(0.01f);

        player.SetActive(true);
        // 재활성화 시 애니메이션 값이 초기화 됌
        player.GetComponent<PlayerController>().animator.SetFloat("AtkSpeed",
            player.GetComponent<PlayerController>().characterBase.atkSpeed);

        fadeManager.FadeIn();
        SpawnManager.Instance.MonsterAllSetActive();

        stageTextAnimation.StageAnimationStart(currentStage.ToString() + " - Stage");
    }

    // 이 과정을 통해서 플레이어 몬스터 리스트는 스폰매니저의 몬스터 리스트를 참조한다.
    public void BattleStart()
    {
        SpawnManager.Instance.MonsterAIStart();
        player.GetComponent<PlayerTargeting>().monsterList = SpawnManager.Instance.monsterList;

        // 플래이어가 배틀 진입 후 기존에 있던 작은 방의 문이 닫히거나
        // 큐브로 막혀서 못 이동하게 설정 및 애니메이션 효과 추가
    }

    // 포탈 나옴 (포탈 관련 효과 및 애니메이션 재생 필요)
    public void StageClear()
    {
        if(lastStage == currentStage)
        {
            BattleFieldEnd(true);
            return;
        }
        
        //플레이어 체력 회복 시킴 전체 체력의 퍼센트(10 or 20퍼)
        player.GetComponent<PlayerController>().PlayerHill(10f);

        portal.SetActive(true);
        portal.transform.GetChild(0).GetComponent<Animator>().SetBool("Clear", true);
    }

    public void BattleFieldEnd(bool clear)
    {
        battleResult.SetResultUI(clear);
        battleResult.StartCoroutine(battleResult.CoResultOpen());
    }
}
