using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform spawnTransform;

    public List<Transform> points;
    public List<GameObject> monsterList;

    public GameObject monsterPrefab;
    public string monsterName;

    public float createTime;

    private int maxMonsterCount;
    private int nowMonsterCount;

    public bool isSpawnEnd;
    public bool isGameOver = false;

    /* 
    추가 해야하는 기능
    1. Instantiate()가 아닌 오브젝트 풀을 사용하여 몬스터를 Pos 지역에 Spawn
    */

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
        /*
            if(maxMonsterCount > nowMonsterCount)
            {
                yield return new WaitForSeconds(1f);

                // 오브젝트 풀로 교체해야 하는 부분
                //GameObject newMonster = ObjectPool.Instance.PopFromPool(monsterName);
                newMonster.transform.parent = points[nowMonsterCount].transform;
                newMonster.SetActive(true);

                nowMonsterCount++;
            }
            else
                yield return null;
        */

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

    public void MonsterDie(GameObject monster)
    {
        Debug.Log(monster.transform.parent);
        monsterList.Remove(monster);

        // 오브젝트 풀에 반납해야함
        
    }
}
