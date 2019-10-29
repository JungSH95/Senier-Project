using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필드 관리
public class FieldManager : Singleton<FieldManager>
{
    public GameObject nowField;

    // base battle 방 시작 위치
    public List<Transform> startPosList;
    // 추후에 캐릭터 얻을 수 있는 이벤트 방, 보스 방 생성 필요

    public GameObject player;
    public GameObject portal;

    public int currentField;
    public int lastField;

    public bool isClear;

    private FadeManager fadeManager;

    // 몬스터가 사망하고 남아 있는 몬스터가 없을 경우 다음 field로 이동하는 포탈이 나옴

    private void Awake()
    {
        currentField = 0;
        lastField = 5;

        player = GameObject.FindGameObjectWithTag("Player");
        fadeManager = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<FadeManager>();
    }

    public void NextField()
    {
        currentField++;

        // 마지막 스테이지 클리어 시
        if (currentField > lastField)
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
        // 부모 오브젝트 얻어오는 방법
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

        fadeManager.FadeIn();
        SpawnManager.Instance.MonsterAllSetActive();
    }

    // 나중에 전투 지역으로 들어갔을 경우에 몬스터 리스트를 받아오게 끔 변경
    // 이 과정을 통해서 플레이어 몬스터 리스트는 스폰매니저의 몬스터 리스트를 참조한다.
    public void BattleStart()
    {
        Debug.Log("전투 시작");
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
