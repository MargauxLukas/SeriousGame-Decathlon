using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulantGeneral : MonoBehaviour
{
    public float speed;
    public bool doesStop;

    private void Update()
    {
        if(doesStop)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if(!GetComponent<BoxCollider2D>().isActiveAndEnabled)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision != null && collision.tag == "Colis" && !doesStop)
        {
            collision.transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
    }
}
