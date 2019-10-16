using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://keidy.tistory.com/205
// https://www.youtube.com/watch?v=mjnTPQipkcU      게임이 망했다 플레이 영상
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
