using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : EnemyBase
{
    public enum State { Idle, Move, Attack, Dead };
    public State currentState = State.Idle;
    
    protected void Start()
    {
        base.Start();
    }

    protected virtual void InitMonster() { }

    protected virtual IEnumerator FSM()
    {
        yield return null;

        InitMonster();

        while(true)
        {            
            if (currentHp <= 0)
                currentState = State.Dead;

            yield return StartCoroutine(currentState.ToString());
        }
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            animator.SetInteger("animation", 1);

        // 공격 가능 상태 (사거리 내에 플레이어 있음)
        if (CanAtkState())
        {
            if (canAtk)
                currentState = State.Attack;
            else
            {
                currentState = State.Idle;
                transform.LookAt(Player.transform.position);
            }
        }
        else
            currentState = State.Move;
    }

    protected virtual IEnumerator Move()
    {
        yield return null;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            animator.SetInteger("animation", 15);


        if (CanAtkState() && canAtk)
            currentState = State.Attack;
        else                                                // 추적 상태
        {
            navAgent.SetDestination(Player.transform.position);
            transform.LookAt(Player.transform);
        }
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;

        canAtk = false;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ATK1"))
            animator.SetInteger("animation", 11);

        monsterAtkSphere.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        currentState = State.Idle;
    }

    protected virtual IEnumerator Dead()
    {
        animator.SetInteger("animation", 7);
        SpawnManager.Instance.MonsterDie(this.gameObject);
        navAgent.enabled = false;
        enemyHpBar.gameObject.SetActive(false);
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(5f);

        this.gameObject.transform.parent.gameObject.SetActive(false);
        ObjectPool.Instance.PushToPool("Monster2", this.gameObject.transform.parent.gameObject);        // 오브젝트 풀에 반환

        StopAllCoroutines();
        yield return null;
    }

    public void MonsterCoroutineStart()
    {
        currentState = State.Idle;
        navAgent.enabled = true;
        enemyHpBar.gameObject.SetActive(true);
        enemyHpBar.SetSlider();
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;

        StartCoroutine(FSM());
    }
}
