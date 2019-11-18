using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public Transform spawnTransform;

    public List<Transform> points;
    public List<GameObject> monsterList;
    
    public bool isSpawnEnd;
    public bool isMonsterClear;

    // 문제 발생 왜 포인트를 미친듯이 잡아내는가?
    public void SetSpawnTransform(GameObject fieldObj)
    {
        spawnTransform = fieldObj.transform.Find("Spawn").transform;

        int nSize = spawnTransform.childCount;
        for(int i=0; i< nSize; i++)
            points.Add(spawnTransform.GetChild(i));
        
        isSpawnEnd = false;

        StartCoroutine(CreateMonster());
    }


    // 여기서 몬스터를 꺼내오기 때문에 몬스터의 종류를 어떻게 정해서 꺼내올 것인가?
    IEnumerator CreateMonster()
    {
        for(int monsterCount = 0; monsterCount < points.Count; monsterCount++)
        {
            GameObject newMonster = GetMonsterObject(points[monsterCount].tag);
            newMonster.transform.parent = points[monsterCount].transform;
            newMonster.transform.position = points[monsterCount].position;
            newMonster.transform.Find("Character").gameObject.transform.position = newMonster.transform.position;
            monsterList.Add(newMonster.transform.Find("Character").gameObject);
        }

        isSpawnEnd = true;
        isMonsterClear = false;
        yield return null;
    }

    public GameObject GetMonsterObject(string type)
    {
        switch(type)
        {
            case "MonsterType0":
                return ObjectPool.Instance.PopFromPool("Monster0");
            case "MonsterType1":
                return ObjectPool.Instance.PopFromPool("Monster1");
            case "MonsterType2":
                return ObjectPool.Instance.PopFromPool("Monster2");
            case "MonsterType3":
                return ObjectPool.Instance.PopFromPool("Monster3");
            default:
                return ObjectPool.Instance.PopFromPool("Monster0");
        }
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

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.currentScene == "2_BattleField")
            {
                FieldManager.Instance.monsterDeadCount++;
                FieldManager.Instance.expCount += monster.GetComponent<EnemyBase>().haveExp;

                // 남아있는 몬스터의 수가 없으면 스테이지 클리어
                if (monsterList.Count == 0)
                    FieldManager.Instance.StageClear();
            }
            else
            {
                if (monsterList.Count == 0)
                    TutorialManager.Instance.TutorialFieldEnd(true);
            }
        }

        
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
