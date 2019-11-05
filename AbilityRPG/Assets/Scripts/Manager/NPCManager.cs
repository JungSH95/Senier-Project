using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public List<GameObject> npcObjList;

    private void Start()
    {
        if (JsonManager.Instance == null)
        {
            Debug.Log("파일 관리자 없음");
            return;
        }

        for (int i = 0; i < npcObjList.Count; i++)
        {
            // 사용 가능한 캐릭터 && 현재 선택된 캐릭터가 아닌 것
            if(JsonManager.Instance.playerData.characterUsed[i]
                && JsonManager.Instance.playerData.characterNumber != i)
                npcObjList[i].SetActive(true);
            else
                npcObjList[i].SetActive(false);
        }
    }

    public void CharacterChange(int nowNumber, int newNumber)
    {
        npcObjList[nowNumber].SetActive(true);
        npcObjList[newNumber].SetActive(false);
    }
}
