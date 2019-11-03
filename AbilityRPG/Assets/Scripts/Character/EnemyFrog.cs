using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFrog : EnemyFSM
{
    // 사거리 표시
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerRealizeRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Start()
    {
        base.Start();

        playerRealizeRange = 2f;
        navAgent.stoppingDistance = 0.45f;

        attackCoolTime = 1f;
        attackCoolTimeCacl = 1f;
    }

    // 몬스터 능력치 조정
    protected override void InitMonster()
    {
        maxHp = 100f;
        currentHp = maxHp;
    }

    // 공격 받은 거 처리
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Weapon0"))
        {
            enemyHpBar.Dmg(30f);
            currentHp -= 30f;
            Instantiate(EffectSet.Instance.LionDmgEffect, collision.contacts[0].point, Quaternion.Euler(90, 0, 0));
        }

        if (collision.transform.CompareTag("Weapon1"))
        {
        }

        if (collision.transform.CompareTag("Weapon2"))
        {
        }
    }
}