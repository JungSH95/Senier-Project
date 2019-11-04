using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    // 캐릭터 기본 정보들 !
    public float maxHp = 100f;
    public float currentHp = 100f;

    public float damage = 10f;

    protected float moveSpeed = 2f;

    private string weapon = "Weapon0";
}
