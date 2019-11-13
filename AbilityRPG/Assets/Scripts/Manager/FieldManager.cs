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

    private int currentStage;
    private int lastStage;

    public bool isClear;

    private FadeManager fadeManager;
    public StageTextAnimation stageAnimation;
    
    private void Awake()
    {
        currentStage = 0;
        lastStage = 5;

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
            player.GetComponent<PlayerController>().characterBase =
                GameManager.Instance.characterInfoList[GameManager.Instance.playerData.characterNumber];
        }
        
        fadeManager = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
    }

    private void Update()
    {
        
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
        
        nowField.SetActive(false);
        player.SetActive(false);    // 조이스틱 이동과 중복 되서 위치 이동 이상하게 됌 그래서 추가
        player.GetComponent<PlayerTargeting>().monsterList = null;

        yield return new WaitForSeconds(0.5f);      // 시각적으로 보이는 것 때문에 일부로 딜레이

        int randomIndex = Random.Range(0, startPosList.Count);
        nowField = startPosList[randomIndex].parent.gameObject;
        nowField.SetActive(true);

        player.transform.position = startPosList[randomIndex].position;
        startPosList.RemoveAt(randomIndex);

        // 포탈 Obj 얻어오기
        portal = nowField.transform.Find("NextFieldPortal").gameObject;

        // 스폰 위치 설정 및 스폰 시작
        SpawnManager.Instance.SetSpawnTransform(nowField);
        SpawnManager.Instance.SpawnStart();

        // 몬스터 생성이 끝날때까지 대기
        while (SpawnManager.Instance.isSpawnEnd != true)
        {
            yield return new WaitForSeconds(0.01f);
        }

        player.SetActive(true);
        player.GetComponent<PlayerController>().animator.SetFloat("AtkSpeed", 
            player.GetComponent<PlayerController>().characterBase.atkSpeed);

        fadeManager.FadeIn();
        SpawnManager.Instance.MonsterAllSetActive();
        stageAnimation.StageAnimationStart(currentStage.ToString());
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
    public void FieldClear()
    {
        portal.SetActive(true);
    }
}
