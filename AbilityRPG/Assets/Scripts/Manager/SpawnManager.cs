using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform spawnTransform;

    public List<Transform> points;
    public List<GameObject> monsterList;

    private int maxMonsterCount;
    private int nowMonsterCount;

    public bool isSpawnEnd;
    public bool isMonsterClear;

    // 문제 발생 왜 포인트를 미친듯이 잡아내는가?
    public void SetSpawnTransform(GameObject fieldObj)
    {
        spawnTransform = fieldObj.transform.Find("Spawn").transform;

        //spawnTransform.GetComponentsInChildren<Transform>(points);
        int nSize = spawnTransform.childCount;

        for(int i=0; i< nSize; i++)
            points.Add(spawnTransform.GetChild(i));

        // GetComponentsInChildren<>() 함수는 부모까지 포함하여 리스트에 넣어준다.
        //points.RemoveAt(0);

        Debug.Log("points 수 : " + points.Count);

        isSpawnEnd = false;

        StartCoroutine(CreateMonster());
    }


    // 여기서 몬스터를 꺼내오기 때문에 몬스터의 종류를 어떻게 정해서 꺼내올 것인가?
    IEnumerator CreateMonster()
    {
        maxMonsterCount = points.Count;
        nowMonsterCount = 0;

        yield return new WaitForSeconds(1f);

        while (nowMonsterCount < maxMonsterCount)
        {
            int randomIndex = Random.Range(1, 3);
            GameObject newMonster = ObjectPool.Instance.PopFromPool("Monster" + randomIndex.ToString());
            newMonster.transform.parent = points[nowMonsterCount].transform;
            newMonster.transform.position = points[nowMonsterCount].position;
            newMonster.transform.Find("Character").gameObject.transform.position = newMonster.transform.position;
            nowMonsterCount += 1;
            monsterList.Add(newMonster.transform.Find("Character").gameObject);
        }

        isSpawnEnd = true;
        isMonsterClear = false;
        yield return null;
    }

    public void MonsterAllSetActive()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            monsterList[i].transform.parent.gameObject.SetActive(true);
            monsterList[i].GetComponent<EnemyFSM>().MonsterSet();
        }
    }

    public void MonsterAIStart()
    {
        for (int i = 0; i < monsterList.Count; i++)
            monsterList[i].GetComponent<EnemyFSM>().MonsterCoroutineStart();
    }

    public void MonsterDie(GameObject monster)
    {
        monsterList.Remove(monster);

        FieldManager.Instance.monsterDeadCount++;
        FieldManager.Instance.expCount += monster.GetComponent<EnemyBase>().haveExp;

        // 남아있는 몬스터의 수가 없으면 스테이지 클리어
        if (monsterList.Count == 0)
            FieldManager.Instance.StageClear();
    }

    // 다음 스테이지로 넘어가기전에 Points에 몬스터가 남아있다면 전부 다 반환
    public void PointsMonsterCheck()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].childCount != 0)
            {
                ObjectPool.Instance.PushToPool(points[i].GetChild(0).name, points[i].GetChild(0).gameObject);
            }
        }

        isMonsterClear = true;
        points.Clear();
    }
}
