using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;

    public float offsetY = 45f;
    public float offsetZ = -40f;

    Vector3 cameraPosition;

    private void LateUpdate()
    {
        if (Player == null)
            return;

        cameraPosition.x = Player.transform.position.x;
        cameraPosition.y = Player.transform.position.y + offsetY;
        cameraPosition.z = Player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
    }
}
