﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeConvoyeur : MonoBehaviour
{
    public GameObject minGauge;
    public GameObject maxGauge;

    public Vector3 initialPos;

    public float height = 1;
    private void Start()
    {
        initialPos = transform.position;
    }

    public void Up()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.0025f, transform.position.z);
    }

    public void Down()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.0025f, transform.position.z);
    }

    public void SetPosition(int floor)
    {
        switch(floor)
        {
            case 0:
                height = 1f;
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y, initialPos.z);
                break;
            case 1:
                height = 1.50f;
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 0.70f, initialPos.z);
                break;
            case 2:
                height = 2f;
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 1.40f, initialPos.z);
                break;
            case 3:
                height = 2.50f;
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 2.1f, initialPos.z);
                break;
            /*case 4:
                height = 3f;
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 2.75f, initialPos.z);
                break;*/
            default:
                break;
        }
    }
}
