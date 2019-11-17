using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public List<GameObject> npcObjList;

    private void Start()
    {
        for (int i = 0; i < npcObjList.Count; i++)
        {
            // 사용 가능한 캐릭터 && 현재 선택된 캐릭터가 아닌 것
            if(GameManager.Instance.playerData.characterUsed[i]
                && GameManager.Instance.playerData.characterNumber != i)
                npcObjList[i].SetActive(true);
            else
                npcObjList[i].SetActive(false);
        }

        Debug.Log("Test : NPCManager");
    }

    public void CharacterChange(int nowNumber, int newNumber)
    {
        npcObjList[nowNumber].SetActive(true);
        npcObjList[newNumber].SetActive(false);
    }
}
