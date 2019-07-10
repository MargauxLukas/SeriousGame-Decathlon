using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinDuConvoyeur : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            //Faire la détection des anomalies finales
            Destroy(collision.gameObject);
        }
    }
}
