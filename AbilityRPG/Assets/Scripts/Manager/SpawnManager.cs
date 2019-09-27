using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] points;

    public GameObject monsterPrefab;
    public string monsterName;

    public float createTime;

    private int maxMonsterCount = 10;
    private int nowMonsterCount = 0;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("MonsterSpawnField").GetComponentsInChildren<Transform>();

        if (points.Length > 0)
            StartCoroutine(CreateMonster());
    }

    /* 
    
    추가 해야하는 기능
    1. Pos 에는 한 개의 몬스터만 존재 할 수 있게.

    2. Instantiate()가 아닌 오브젝트 풀을 사용하여 몬스터를 Pos 지역에 Spawn

    3. 만약 특정 Pos의 몬스터가 사망 할 경우 오브젝트 풀을 사용하여 반납하고
       해당 Pos에 대해서 타이머 재생 후 몬스터 ReSpawn
    
    */

    IEnumerator CreateMonster()
    {
        while(!isGameOver)
        {
            if(maxMonsterCount > nowMonsterCount)
            {
                yield return new WaitForSeconds(1f);

                int idx = Random.Range(1, points.Length);

                // 오브젝트 풀로 교체해야 하는 부분
                //GameObject newMonster = Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
                GameObject newMonster = ObjectPool.Instance.PopFromPool(monsterName);
                //newMonster.transform.parent = points[idx].transform;
                newMonster.SetActive(true);

                nowMonsterCount++;
            }
            else
                yield return null;
        }

        yield return null;
    }
}
