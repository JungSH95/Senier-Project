using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour {

    // 캐릭터 기본 정보들 !
    public float maxHp = 100f;
    public float currentHp = 100f;

    public float damage = 10f;

    [SerializeField]
    protected float playerRealizeRange = 2f;        // 플레이어 인식 거리
    protected float attackRange = 0.5f;             // 공격 사거리
    protected float attackCoolTime = 3f;            // 공격 쿨타임
    protected float attackCoolTimeCacl = 3f;        // 코루틴 : 공격 쿨타임
    protected bool canAtk = true;                   // 공격 가능 상태 유무
    
    protected float moveSpeed = 2f;                 // 이동속도
    [SerializeField]
    protected float distance;

    protected GameObject Player;

    protected NavMeshAgent navAgent;
    protected Animator animator;
    protected EnemyHpBar enemyHpBar;

    public LayerMask layerMask;

    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHpBar = gameObject.transform.parent.Find("Canvas").GetComponent<EnemyHpBar>();

        StartCoroutine(CoCoolTime());
    }

    protected bool CanAtkState()
    {
        Vector3 targetDir = new Vector3(Player.transform.position.x - transform.position.x, 0f,
            Player.transform.position.z - transform.position.z);
        RaycastHit hit;
        Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), targetDir,
            out hit, 30f, layerMask);
        distance = Vector3.Distance(Player.transform.position, transform.position);

        // 맞은 물체가 없을 경우
        if (hit.transform == null)
            return false;

        // 플레이어 사정거리 안
        if (hit.transform.CompareTag("Player") && distance <= attackRange)
            return true;
        else
            return false;
    }

    // 공격 쿨타임 돌리기
    public virtual IEnumerator CoCoolTime()
    {
        while(true)
        {
            yield return null;

            // 공격 쿨타임일 경우
            if(!canAtk)
            {
                attackCoolTimeCacl -= Time.deltaTime;
                if(attackCoolTimeCacl <= 0)
                {
                    attackCoolTimeCacl = attackCoolTime;
                    canAtk = true;
                }
            }
        }
    }
}
