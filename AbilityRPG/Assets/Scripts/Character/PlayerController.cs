using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2.0f;         // 움직이는 스피드.
    
    protected Joystick joystick;
    protected Joybutton joybutton;

    private Rigidbody rigidbody;
    private Animator animator;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    public void FixedUpdate()
    {
        animator.SetBool("MOVE", false);

        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            animator.SetBool("MOVE", true);

            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
        }
    }
}
