using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour
{
    public void PlayerInfoButton()
    {
        MainFieldManager.Instance.PlayerInfoOpen();
    }
}