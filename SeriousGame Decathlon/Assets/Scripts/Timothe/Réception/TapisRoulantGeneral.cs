using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulantGeneral : MonoBehaviour
{
    public float speed;
    public bool doesStop;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Colis" && !doesStop)
        {
            collision.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
    }
}
