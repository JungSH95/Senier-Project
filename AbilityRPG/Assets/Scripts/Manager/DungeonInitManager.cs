using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://aniz.tistory.com/357  알고리즘 종류
// https://www.youtube.com/watch?v=qAf9axsyijY 아이작 형식의 맵 제작
// 유튜브 검색어 : unity dungeon generator
public class DungeonInitManager : MonoBehaviour
{
    // 던전 렙에 따라서 방의 수가 정해짐
    public int dungeonLevel;
    public int fieldCount;

    // 필드 종류 T:Top, B:Bottom, L:Left, R:Right
    public GameObject Start_Field;

    // 이전 필드의 포탈 위치가 T, B, L, R 일 경우 연결 가능한 필드 리스트
    public List<GameObject> T_Field;        
    public List<GameObject> B_Field;
    public List<GameObject> L_Field;
    public List<GameObject> R_Field;

    public GameObject LTRB_Field;


    private void Awake()
    {
        dungeonLevel = 1;
        fieldCount = dungeonLevel * 3;
    }

    public void Start()
    {
        //DungeonInit();

        //StartCoroutine(coE());

        //GameObject obj = Instantiate(R_Field);
        //obj.transform.position = new Vector3(0f, -0.2f, 3f);
    }

    public void DungeonInit()
    {
        
    }

    // 포탈 연결 과정은 어떻게?

    public IEnumerator coE()
    {
        for (int i = 0; i < 20; i++)
        {
            //GameObject obj = Instantiate(R_Field);
            //obj.transform.position = new Vector3(0f, -0.2f, 10f * (i + 1));
            //obj.SetActive(false);
        }

        yield return null;
    }
}
