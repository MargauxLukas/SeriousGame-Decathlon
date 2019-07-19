using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementNuages : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x >= 10.37f)
        {
            transform.localPosition = new Vector3(-1.97f, transform.localPosition.y, 0);
        }
        transform.localPosition += new Vector3(1, 0, 0) * speed;
    }
}
