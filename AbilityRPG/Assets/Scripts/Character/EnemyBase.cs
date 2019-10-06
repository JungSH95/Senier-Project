using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {

    // 캐릭터 기본 정보들 !
    public float maxHp = 100f;
    public float currentHp = 100f;

    public float damage = 10f;

    protected float playerRealizeRange = 2f;
    protected float attackRange = 0.5f;
    protected float attackCoolTime = 3f;
    protected bool canAtk = true;

    protected float moveSpeed;
}
