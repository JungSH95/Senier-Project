using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2.0f;         // 움직이는 스피드.
    
    protected Joystick joystick;
    protected Joybutton joybutton;

    private Rigidbody rigidbody;
    public Animator animator;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();

        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    public void FixedUpdate()
    {
        if (isPlayerMoving())
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Monster"))
            Debug.Log("몬스터로 공격 받음");
    }

    public bool isPlayerMoving()
    {
        if (joystick.Vertical != 0 || joystick.Horizontal != 0)
        {
            Debug.Log("이동중");
            return true;
        }
        else
        {
            Debug.Log("이동중 아님");
            return false;
        }
    }
}
