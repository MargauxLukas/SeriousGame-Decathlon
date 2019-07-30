using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanRfidGtp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ArticleGTP")
        {
            if(collision.GetComponent<ArticleUnitGTP>()!=null)
            {
                collision.GetComponent<ArticleUnitGTP>().hasBeenScanned = true;
                Debug.Log("HasBeenScanned");
            }
        }
    }
}
