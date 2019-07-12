using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvoieColisSceneGenerale : MonoBehaviour
{
    public Transform newPosition;

    public TapisRoulant tapis;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            collision.transform.position = newPosition.position;
            collision.GetComponent<ScriptColisRecep>().isOneSecondScreen = true;
            tapis.colisSurLeTapis.Remove(collision.gameObject);
            tapis.colisEnvoye.Add(collision.gameObject);
        }
    }
}
