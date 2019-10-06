using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public List<GameObject> monsterList;

    float currentDist = 0f;      // 현재거리
    float closetDist = 10f;     // 가까운 거리
    float targetDist = 10f;     // 타겟 거리

    int closetDistIndex = 0;    // 가장 가까운 인덱스
    int targetIndex = -1;       // 타겟 할 인덱스

    public LayerMask layerMask;

    private void OnDrawGizmos()
    {
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

    void Update()
    {
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

                if (isHit && hit.transform.CompareTag("Monster"))
                    Debug.Log(hit.transform.name);
            }
        }
    }
}
