using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public void EffectStart()
    {
        if (EffectSet.Instance.PortalEffect != null)
            Instantiate(EffectSet.Instance.PortalEffect, transform.position, Quaternion.Euler(0, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            FieldManager.Instance.NextField();
    }
}