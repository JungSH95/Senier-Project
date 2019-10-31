﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLion : EnemyFSM
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

        Debug.Log("몬스터 생성");
    }

    // 공격 받은 거 처리
    private void OnCollisionEnter(Collision collision)
    {

    }

    public void HitPlayerAttack(float atk)
    {
        currentHp -= atk;
        enemyHpBar.Dmg(atk);
    }
}
