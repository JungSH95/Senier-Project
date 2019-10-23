using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재 필드에서의 포탈 관리 및 몬스터 스폰 관리?
public class FieldManager : MonoBehaviour
{
    public enum DoorPos { TOP, RIGHT, BOTTOM, LEFT};

    public struct FieldInfo
    {
        DoorPos doorPos;
    }

    private void Awake()
    {
        Debug.Log("비활성중 Awake");
    }

    public void Start()
    {
        Debug.Log("비활성중 Start");
    }

    public void test()
    {
        Debug.Log("비활성중 함수 테스트");
    }
}
