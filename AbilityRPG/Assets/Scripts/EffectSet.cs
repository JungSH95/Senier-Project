using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSet : Singleton<EffectSet>
{
    [Header("Player")]
    public GameObject PlayerAtkEffect;
    public GameObject PlayerDmgEffect;

    [Header("Monster")]
    public GameObject LionAtkEffect;
    public GameObject LionDmgEffect;

    public GameObject PortalEffect;
}
