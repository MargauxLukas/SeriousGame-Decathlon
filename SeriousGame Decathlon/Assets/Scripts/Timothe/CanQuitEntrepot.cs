using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanQuitEntrepot : MonoBehaviour
{
    public GameObject boutonQuitter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Unit>() != null)
        {
            boutonQuitter.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Unit>() != null)
        {
            boutonQuitter.SetActive(false);
        }
    }
}
