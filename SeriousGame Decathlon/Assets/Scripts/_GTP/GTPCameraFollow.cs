using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTPCameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offset = -0.5f;

    private float maxY = 6f;
    private float minY = -4f;

    private float maxX = 20f;
    private float minX = -8f;

    void Update()
    {
        // Y déplacement
        if (player.transform.position.y < minY)
        {
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
        }
        else if (player.transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, 6f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + offset, transform.position.z);
        }

        // X déplacement
        if (player.transform.position.x < minX)
        {
            Debug.Log("Min");
            transform.position = new Vector3(-8f, transform.position.y, transform.position.z);
        }
        else if (player.transform.position.x > maxX)
        {
            Debug.Log("Max");
            transform.position = new Vector3(20f ,transform.position.y , transform.position.z);
        }
        else
        {
            Debug.Log("Else");
            transform.position = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);
        }
    }
}
