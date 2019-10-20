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

    // 보유한 필드 오브젝트 리스트
    public List<GameObject> fieldObjectList;

    /*
     *  1. 각 필드에는 포탈이 존재
     *  
     *  2. 해당 포탈들을 다른 필드로 연결하는 과정을 여기서 해야함
     *  -> 필요한 것 : 해당 필드들의 포탈 정보
     *  
     *  3. 방은 랜덤 생성 및 배치
     *  -> 방의 수에 따라서 문의 개수를 설정
     *  -> if) Start 방으로 시작
     *      1) 남은 방의 개수가 1개일 경우 -> 문 1개
     *      2) 남은 방의 개수가 2개일 경우 -> 문 2개
     *      3) 남은 방의 개수가 3개일 경우 -> 문 2개 or 3개
     *          -> 문 2개일 경우 선택시 방의 개수 -1 후 2개가 남음
     *          -> 2)번의 선택지로 끝남
     *          
     *          -> 문 3개일 경우 해당 방에 남은 포탈 2개 == 남은 방의 수
     *          -> 각 문 1개짜리 방 만들어주고 끝
     *          
     *      4) 남은 방의 개수가 4개일 경우 -> 문 2개 or 3개 or 4개
     *      
     *  4. 던전 맵 제작을 할 것인가?
     */

    void Start()
    {
        
    }
}
