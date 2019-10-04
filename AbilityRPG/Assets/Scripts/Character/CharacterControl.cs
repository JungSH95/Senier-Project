using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float Speed = 2.0f;         // 움직이는 스피드.
    
    protected Joystick joystick;
    protected Joybutton joybutton;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
    }

    
    public void Update()
    {
        float x = joystick.Horizontal;
        float z = joystick.Vertical;
        transform.Translate(new Vector3(x, transform.position.y, z) * Time.deltaTime * Speed);
    }
}
