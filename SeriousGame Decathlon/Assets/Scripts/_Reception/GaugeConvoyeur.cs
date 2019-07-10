using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeConvoyeur : MonoBehaviour
{
    public GameObject minGauge;
    public GameObject maxGauge;

    public float height = 1;
    private float minY;
    private float maxY;

    private void Start()
    {
        minY = minGauge.transform.position.y;
        maxY = maxGauge.transform.position.y;
    }

    public void Up()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.0025f, transform.position.z);
    }

    public void Down()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.0025f, transform.position.z);
    }
}
