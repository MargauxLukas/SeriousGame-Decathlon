using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AffichageMoniteurAleatoire
{
    public List<Sprite> currentMoniteur;
}

public class MoniteurChoixAnimation : MonoBehaviour
{
    public int numMoniteur;
    public List<AffichageMoniteurAleatoire> moniteurProcessActuel;

    private void Start()
    {
        numMoniteur = Random.Range(0, moniteurProcessActuel.Count);
        GetComponent<SpriteRenderer>().sprite = moniteurProcessActuel[numMoniteur].currentMoniteur[0];
        StartCoroutine(ChangementSprite(2));
    }

    IEnumerator ChangementSprite(int timeToWait)
    {
        GetComponent<SpriteRenderer>().sprite = moniteurProcessActuel[numMoniteur].currentMoniteur[Random.Range(0,4)];
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(ChangementSprite(Random.Range(2, 5)));
    }
}
