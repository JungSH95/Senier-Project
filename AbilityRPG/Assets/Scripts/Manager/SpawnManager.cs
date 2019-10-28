using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform spawnTransform;

    public List<Transform> points;
    public List<GameObject> monsterList;
    
    public string monsterName;

    private int maxMonsterCount;
    private int nowMonsterCount;

    public bool isSpawnEnd;

    public void SetSpawnTransform(GameObject fieldObj)
    {
        spawnTransform = fieldObj.transform.Find("Spawn").transform;
        spawnTransform.GetComponentsInChildren<Transform>(points);

        // GetComponentsInChildren<>() 함수는 부모까지 포함하여 리스트에 넣어준다.
        points.RemoveAt(0);
    }

    public void SpawnStart()
    {
        Debug.Log("스폰 시작");

        isSpawnEnd = false;

        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        maxMonsterCount = points.Count;
        nowMonsterCount = 0;

        while (nowMonsterCount < maxMonsterCount)
        {
            //GameObject newMonster = Instantiate(monsterPrefab, points[nowMonsterCount].position, points[nowMonsterCount].rotation);
            GameObject newMonster = ObjectPool.Instance.PopFromPool("Monster2");
            newMonster.transform.parent = points[nowMonsterCount].transform;
            newMonster.transform.position = points[nowMonsterCount++].position;
            monsterList.Add(newMonster);
        }

        isSpawnEnd = true;

        yield return null;
    }

    public void MonsterAllSetActive()
    {
        for (int i = 0; i < monsterList.Count; i++)
            monsterList[i].SetActive(true);
    }

    public void MonsterAIStart()
    {
        for (int i = 0; i < monsterList.Count; i++)
            monsterList[i].GetComponent<EnemyFSM>().MonsterCoroutineStart();
    }

    public void MonsterDie(GameObject monster)
    {
        Debug.Log(monster.transform.parent);
        monsterList.Remove(monster);

        // 남아있는 몬스터의 수가 없을 경우 다음 필드로 이동하기 위한 포탈 생성
        if (monsterList.Count == 0)
            FieldManager.Instance.FieldClear();
    }
}
