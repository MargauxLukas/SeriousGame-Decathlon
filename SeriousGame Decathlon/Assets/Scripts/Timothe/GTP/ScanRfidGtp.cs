using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanRfidGtp : MonoBehaviour
{
    public AudioSource source;
    public AudioClip musique;
    public float coefSound = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ArticleGTP")
        {
            if(collision.GetComponent<ArticleUnitGTP>()!=null)
            {
                if (musique != null && source != null)
                {
                    source.clip = musique;
                    source.volume = coefSound;
                    source.Play();
                }
                collision.GetComponent<ArticleUnitGTP>().hasBeenScanned = true;
                StartCoroutine(AffichageEclairage());
                Debug.Log("HasBeenScanned");
            }
        }
    }

    IEnumerator AffichageEclairage()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.7f);
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
