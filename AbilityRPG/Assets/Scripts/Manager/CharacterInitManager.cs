using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInitManager : MonoBehaviour
{
    private FadeManager fadeManager;

    void Start()
    {
        fadeManager = GameObject.Find("FadeManager").GetComponent<FadeManager>();
        fadeManager.FadeIn();
    }
}
