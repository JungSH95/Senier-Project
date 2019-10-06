using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float Speed = 2.0f;         // 움직이는 스피드.
    
    protected Joystick joystick;
    protected Joybutton joybutton;

    private Rigidbody rigidbody;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        rigidbody = GetComponent<Rigidbody>();
    }


    public void FixedUpdate()
    {
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            //Vector3 vector3 = Vector3.zero;
            //vector3.Set(joystick.Horizontal, 0, joystick.Vertical);
            //vector3 = vector3.normalized * Speed * Time.deltaTime;
            //rigidbody.MovePosition(transform.position + vector3);
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
        }
    }
}
