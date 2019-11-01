using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Rigidbody rigidbody;

    public void Shoot()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * 5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster"))
        {
            rigidbody.velocity = Vector3.zero;
            
            ObjectPool.Instance.PushToPool(gameObject.name, this.gameObject);
        }
    }
}
