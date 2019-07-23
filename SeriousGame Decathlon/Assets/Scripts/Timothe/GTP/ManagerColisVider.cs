using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColisVider : MonoBehaviour
{
    private List<bool> etatColis = new List<bool>();

    public List<GameObject> colisAnimationVenir;

    public List<Transform> positionParEmmplacements;
    public List<GameObject> emplacementsScripts;

    public List<Colis> colisVider;
    private int nbCurrentColis = 0;
    public int chanceColisPasRemplit;

    public ManagerColisAttendu managerColis;

    // Start is called before the first frame update
    void Start()
    {
        etatColis.Add(false);
        etatColis.Add(false);
        StartCoroutine(FaireVenirPremiersColis());
    }

    public IEnumerator FaireVenirPremiersColis()
    {
        FaireVenirNouveauColis(0);
        StartCoroutine(ActiverAutreColis(0));
        yield return new WaitForSeconds(3f);
        FaireVenirNouveauColis(1);
    }

    public Colis ChoixNouveauColis()
    {
        Colis newColis = new Colis();
        if (colisVider != null)
        {
            if (colisVider.Count > 1)
            {
                newColis = Instantiate(colisVider[Random.Range(0, colisVider.Count - 1)]);
            }
            else if (colisVider.Count > 0)
            {
                newColis = Instantiate(colisVider[0]);
            }
        }
        if(Random.Range(0, 100) < chanceColisPasRemplit)
        {
            int nbArticleDebut = newColis.listArticles.Count;
            for (int i = 0; i < nbArticleDebut / 2; i++)
            {
                newColis.listArticles.RemoveAt(newColis.listArticles.Count);
            }
        }
        return newColis;
    }

    void FaireVenirNouveauColis(int emplacement)
    {
        etatColis[emplacement] = true;
        StartCoroutine(colisAnimationVenir[emplacement].GetComponent<AnimationFaireVenirColis>().AnimationColis(emplacementsScripts[emplacement]));
        if (colisVider != null)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis();
        }
    }


    void FairePartirUnColis(int emplacement)
    {
        etatColis[emplacement] = false;
        emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().enabled = false;
        emplacementsScripts[emplacement].SetActive(false);
        if(colisVider != null)
        {
            //emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis();
        }
        if (emplacementsScripts[(emplacement + 1) % 2] != null)
        {
            StartCoroutine(ActiverAutreColis((emplacement + 1) % 2));
        }
    }

    IEnumerator ActiverAutreColis(int emplacement)
    {
        for(int m = 0; m < 100; m++)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().enabled = true;
            emplacementsScripts[emplacement].transform.position += new Vector3(0, 1, 0)  * Time.deltaTime * 0.3f;
            if(m>=99)
            {
                managerColis.AjoutArticleColisVoulu(emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0], Random.Range(0,2), Random.Range(1,7));
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
