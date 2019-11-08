using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFieldManager : MonoBehaviour
{
    public List<GameObject> characterList;
    public GameObject player;

    private void Awake()
    {
        // 테스트 위해서 (임시 방편으로 만들긴 했는데 되긴 함)
        if (GameManager.Instance == null)
        {
            player = Instantiate(characterList[0]);
        }
        else
        {
            player = Instantiate(characterList[GameManager.Instance.playerData.characterNumber]);
            player.GetComponent<PlayerController>().characterBase =
                GameManager.Instance.characterInfoList[GameManager.Instance.playerData.characterNumber];
        }
    }
}
