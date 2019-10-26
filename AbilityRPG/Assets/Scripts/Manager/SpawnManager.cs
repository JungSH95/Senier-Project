using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
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

    // Start is called before the first frame update
    void Awake()
    {
        //this.gameObject.GetComponentsInChildren<Transform>(points);
        spawnTransform.GetComponentsInChildren<Transform>(points);
        // GetComponentsInChildren<>() 함수는 부모까지 포함하여 리스트에 넣어준다.
        points.RemoveAt(0);

        isSpawnEnd = false;
        maxMonsterCount = points.Count;
        nowMonsterCount = 0;
    }

    /* 
    
    추가 해야하는 기능
    1. Pos 에는 한 개의 몬스터만 존재 할 수 있게.

    2. Instantiate()가 아닌 오브젝트 풀을 사용하여 몬스터를 Pos 지역에 Spawn

    3. 만약 특정 Pos의 몬스터가 사망 할 경우 오브젝트 풀을 사용하여 반납하고
       해당 Pos에 대해서 타이머 재생 후 몬스터 ReSpawn
    
    */

    public void SpawnStart()
    {
        Debug.Log("스폰 시작");

        isSpawnEnd = false;

        StartCoroutine(CreateMonster());
    }

    IEnumerator CreateMonster()
    {
        /*
        while(!isGameOver)
        {
            if(maxMonsterCount > nowMonsterCount)
            {
                yield return new WaitForSeconds(1f);

                //int idx = Random.Range(1, points.Count);

                // 오브젝트 풀로 교체해야 하는 부분
                GameObject newMonster = Instantiate(monsterPrefab, points[nowMonsterCount].position, points[nowMonsterCount].rotation);
                //GameObject newMonster = ObjectPool.Instance.PopFromPool(monsterName);
                newMonster.transform.parent = points[nowMonsterCount].transform;
                newMonster.SetActive(true);

                nowMonsterCount++;
            }
            else
                yield return null;
        }
        */

        //yield return new WaitForSeconds(0.5f);
        /*
        while(points.Count == 0)
        {
            Debug.Log("스폰 포인트 0");
            yield return new WaitForSeconds(0.1f);
        }
        */
        
        maxMonsterCount = points.Count;
        nowMonsterCount = 0;

        while (nowMonsterCount < maxMonsterCount)
        {
            GameObject newMonster = Instantiate(monsterPrefab, points[nowMonsterCount].position, points[nowMonsterCount].rotation);
            newMonster.transform.parent = points[nowMonsterCount++].transform;
            monsterList.Add(newMonster);

            Debug.Log("몬스터 수 : " + monsterList.Count);
        }

        isSpawnEnd = true;

        yield return null;
    }

    public void MonsterAllSetActive()
    {
        for (int i = 0; i < monsterList.Count; i++)
            monsterList[i].SetActive(true);
    }
}
