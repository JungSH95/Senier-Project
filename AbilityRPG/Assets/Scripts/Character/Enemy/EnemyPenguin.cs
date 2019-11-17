using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPenguin : EnemyFSM
{
    private new void Start()
    {
        base.Start();

        haveExp = 1;

        attackRange = 0.55f;
        navAgent.stoppingDistance = 0.5f;

        attackCoolTime = 3f;
        attackCoolTimeCacl = 3f;
    }

    // 몬스터 능력치 조정
    protected override void InitMonster()
    {
        maxHp = 100f;
        currentHp = maxHp;
    }

    //protected override IEnumerator Attack()
    //{
    //    Debug.Log("호롤롤");
    //    yield return new WaitForSeconds(0.2f);
    //}
}
