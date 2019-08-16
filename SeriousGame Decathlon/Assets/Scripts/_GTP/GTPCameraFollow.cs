using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTPCameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offset = -0.5f;
    public float lerpCoef = 0.05f;

    private Vector3 newPos;

    [SerializeField]
    private float maxY = 6f;
    [SerializeField]
    private float minY = -4f;

    [SerializeField]
    private float maxX = 20f;
    [SerializeField]
    private float minX = -8f;

    private void Start()
    {
        newPos = transform.position;
    }

    void Update()
    {
        // Y déplacement
        if (player.transform.position.y < minY)
        {
            newPos = new Vector3(newPos.x, minY, newPos.z);
        }
        else if (player.transform.position.y > maxY)
        {
            newPos = new Vector3(newPos.x, maxY, newPos.z);
        }
        else
        {
            newPos = new Vector3(newPos.x, player.transform.position.y, newPos.z);
        }

        if (player.transform.position.x < minX)
        {
            newPos = new Vector3(minX, newPos.y, newPos.z);
        }
        else if (player.transform.position.x > maxX)
        {
            newPos = new Vector3(maxX, newPos.y, newPos.z);
        }
        else
        {
            newPos = new Vector3(player.transform.position.x, newPos.y, newPos.z);
        }
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, newPos.x + offset, lerpCoef), Mathf.Lerp(transform.position.y, newPos.y + offset, lerpCoef), transform.position.z);


    }
}
