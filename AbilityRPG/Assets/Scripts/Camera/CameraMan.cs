using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://truecode.tistory.com/28?category=611884
public class CameraMan : MonoBehaviour {

    public float MoveSpeed;         // 플레이어 따라가는 카메라 맨의 속도

    private Transform Target;       // 플레이어의 위치
    private Vector3 Pos;            // 카메라 맨의 위치

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = 0.1f;
    }

    private void FixedUpdate()
    {
        Pos = transform.position;
        transform.position += (Target.position - Pos) * MoveSpeed;
    }
}
