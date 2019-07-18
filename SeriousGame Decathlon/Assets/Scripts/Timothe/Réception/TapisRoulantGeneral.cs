using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapisRoulantGeneral : MonoBehaviour
{
    public float speed;
    public ConvoyeurButton boutonConvoyeur;
    public ConvoyeurManager convoyeur;

    public void doesStop()
    {
        convoyeur.isOn = false;
        boutonConvoyeur.isOffAmpoule.SetActive(true);
        boutonConvoyeur.isOnAmpoule.SetActive(false);
    }

    private void Update()
    {
        if(!convoyeur.isOn)
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
        if(collision != null && collision.tag == "Colis" && convoyeur.isOn)
        {
            collision.transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
    }
}
