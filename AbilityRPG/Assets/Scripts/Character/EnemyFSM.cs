using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : EnemyBase
{
    public enum State { Idle, Move, Attack, Dead };
    public State currentState = State.Idle;

    protected void Start()
    {
        StartCoroutine(FSM());
    }

    protected virtual void InitMonster() { }

    protected virtual IEnumerator FSM()
    {
        yield return null;
    }

    protected virtual IEnumerator Idle()
    {
        yield return null;
    }

    protected virtual IEnumerator Move()
    {
        yield return null;
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;
    }

    protected virtual IEnumerator Dead()
    {
        yield return null;
    }
}
