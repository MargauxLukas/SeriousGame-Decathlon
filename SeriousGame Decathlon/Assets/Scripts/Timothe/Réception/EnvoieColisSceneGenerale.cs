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
            collision.transform.Rotate(new Vector3(0, 0, 90));
            tapis.colisSurLeTapis.Remove(collision.gameObject);
        }
    }
}
