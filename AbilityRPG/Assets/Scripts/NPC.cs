using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=_nRzoTzeyxU      다이얼로그 시스템 만드는법
// https://keidy.tistory.com/205
// https://www.youtube.com/watch?v=mjnTPQipkcU
public class NPC : MonoBehaviour
{
    public Transform Atransform;

    // Start is called before the first frame update
    void Start()
    {
        Atransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0,0,0);
    }
}
