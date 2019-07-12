using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeColis : MonoBehaviour
{
    public GameObject minGauge;
    public GameObject maxGauge;

    public Vector3 initialPos;

    private float height = 0f;

    public void Start()
    {
        initialPos = transform.position;
    }

    public void SetFloor(int floor)
    {
        switch (floor)
        {
            case 0:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y, initialPos.z);
                height = 0f;
                break;
            case 1:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 0.70f, initialPos.z);
                height = 1f;
                break;
            case 2:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 1.40f, initialPos.z);
                height = 1.5f;
                break;
            case 3:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 2.1f, initialPos.z);
                height = 2f;
                break;
            case 4:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 2.75f, initialPos.z);
                height = 2.50f;
                break;
            case 5:
                transform.position = new Vector3(initialPos.x, minGauge.transform.position.y + 3.4f, initialPos.z);
                height = 3f;
                break;
            default:
                break;
        }
    }
}
