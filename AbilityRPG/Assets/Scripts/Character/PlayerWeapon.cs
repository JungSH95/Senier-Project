using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float damage;

    public void Shoot(float dmg = 1f)
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * 5f;
        damage = dmg;

        // 삭제 안될 경우 방지
        StartCoroutine(CoWeaponDestroy());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Monster")
            || collision.transform.CompareTag("Obstacle"))
        {
            rigidbody.velocity = Vector3.zero;
            
            ObjectPool.Instance.PushToPool(gameObject.name, this.gameObject);
        }
    }

    IEnumerator CoWeaponDestroy()
    {
        yield return new WaitForSeconds(3f);
        ObjectPool.Instance.PushToPool(gameObject.name, this.gameObject);
    }
}
