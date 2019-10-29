﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    // 해당 스테이지에 있는 몬스터를 받아올 리스트
    public List<GameObject> monsterList;

    float currentDist = 0f;      // 현재거리
    float closetDist = 100f;     // 가까운 거리
    float targetDist = 100f;     // 타겟 거리

    int closetDistIndex = 0;    // 가장 가까운 인덱스
    int targetIndex = -1;       // 타겟 할 인덱스

    public bool getTarget = false;

    public LayerMask layerMask;

    private PlayerController playerController;

    // 시각적 표현 (에디터에서)
    private void OnDrawGizmos()
    {
        if (monsterList == null)
            return;

        for (int i = 0; i < monsterList.Count; i++)
        {
            RaycastHit hit;
            bool isHit = Physics.Raycast(transform.position, monsterList[i].transform.position - transform.position,
                out hit, 20f, layerMask);

            if (isHit && hit.transform.CompareTag("Monster"))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawRay(transform.position, monsterList[i].transform.position - transform.position);
        }
    }

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        SetTarget();
        AtkTarget();
    }

    void SetTarget()
    {
        if (monsterList == null)
            return;

        if (monsterList.Count != 0)
        {
            currentDist = 0f;
            closetDistIndex = 0;
            targetIndex = -1;

            for (int i = 0; i < monsterList.Count; i++)
            {
                currentDist = Vector3.Distance(transform.position, monsterList[i].transform.position);
                RaycastHit hit;
                bool isHit = Physics.Raycast(transform.position, monsterList[i].transform.position - transform.position,
                    out hit, 20f, layerMask);

                // 레이저에 맞은 타겟중 장애물에 안걸린 몬스터
                if (isHit && hit.transform.CompareTag("Monster"))
                {
                    if (targetDist >= currentDist)
                    {
                        targetIndex = i;
                        targetDist = currentDist;
                    }
                }

                // 장애물에 상관없이 가까운 몬스터
                if (closetDist >= currentDist)
                {
                    closetDistIndex = i;
                    closetDist = currentDist;
                }
            }

            // 레이저에 맞은 몬스터가 없으면 가장 가까운 몬스터를 향해 이동
            if (targetIndex == -1)
            {
                targetIndex = closetDistIndex;
            }

            // 초기화
            closetDist = 100f;
            targetDist = 100f;

            getTarget = true;
        }
    }

    void AtkTarget()
    {
        if (monsterList == null)
            return;

        // 타겟 없을 경우
        if (targetIndex == -1 || monsterList.Count == 0)  // 추가 
        {
            playerController.animator.SetBool("ATTACK", false);
            return;
        }

        // 
        if (getTarget && !playerController.isPlayerMoving() && monsterList.Count != 0)
        {
            //Debug.Log ( "lookat : " + MonsterList[TargetIndex].transform.GetChild ( 0 ) );  // 변경
            transform.LookAt(monsterList[targetIndex].transform);     // 변경

            //if (playerController.animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE"))
            {
                //Debug.Log("타겟있는데 공격 애니메이션 실행");

                playerController.animator.SetBool("ATTACK", true);
                playerController.animator.SetBool("IDLE", false);
                playerController.animator.SetBool("MOVE", false);
            }

        }
        else if (playerController.isPlayerMoving())
        {
            if (!playerController.animator.GetCurrentAnimatorStateInfo(0).IsName("MOVE"))
            {
                //Debug.Log("이동중 애니메이션 실행");

                playerController.animator.SetBool("ATTACK", false);
                playerController.animator.SetBool("IDLE", false);
                playerController.animator.SetBool("MOVE", true);
            }
        }
        else
        {
            //Debug.Log("아이들");

            playerController.animator.SetBool("ATTACK", false);
            playerController.animator.SetBool("IDLE", true);
            playerController.animator.SetBool("MOVE", false);
        }
    }

    void MonsterATK()
    {
        //Debug.Log("몬스터가 맞았습니다.");

        monsterList[targetIndex].GetComponent<EnemyLion>().currentHp -= 50;

        // 이런식으로 함수 처리할 경우에 장애물이 있는 것은 어떻게 처리 할 것인가?.
        
        // 이 방법은 애니메이션 이벤트를 사용한 방법

        // 다른 방법 2. 온트리거를 사용하여 충돌 체크하여 충돌시 플레이어가 가지고 있는
        // 무기 오브젝트에 맞으면? 이것도 이상함
        // 처리 방법 -> 무기 오브젝트에 맞고 이펙트 발생까지 원만히 처리 된 경우에
        // 데미지 처리
    }
}
