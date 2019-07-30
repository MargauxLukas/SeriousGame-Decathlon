using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offset = -0.5f;

    private float maxY = 25f;
    private float minY = 1f;

    void Update()
    {
        if (player.transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        }
        else if(player.transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, 25f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + offset, transform.position.z);
        }
    }
}
