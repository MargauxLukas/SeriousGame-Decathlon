using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionPlacementColis : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Colis")
        {
            Scoring.instance.RecepMalus(50);
            Scoring.instance.AffichageErreur("Tu as mal positionné ton colis sur le convoyeur");
        }
    }
}
