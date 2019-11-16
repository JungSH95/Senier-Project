using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : Singleton<EffectSet>
{
    [Header("Player")]
    public GameObject PlayerHillEffect;
    public GameObject PlayerDmgEffect;

    [Header("Monster")]
    public GameObject MonsterAtkEffect;
    public List<GameObject> MonsterDmgEffect;

    [Header("Portal")]
    public GameObject PortalEffect;
}
