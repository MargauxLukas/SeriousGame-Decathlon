using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerColisVider : MonoBehaviour
{
    public List<bool> etatColis = new List<bool>();

    public List<GameObject> colisAnimationVenir;

    public List<Transform> positionParEmmplacements;
    public List<GameObject> emplacementsScripts;

    public List<Colis> colisVider;
    private int nbCurrentColis = 0;
    public int chanceColisPasRemplit;
    public int chanceArticlePasBon;

    public Image photoArticle;

    public ManagerColisAttendu managerColis;

    public List<RemplissageColisGTP> colisActuellementsPose;

    public bool aEteVerifier;

    private int emplacement = 0;

    private float tempsReponseChangementColis;
    int emplacementTempo;
    // Start is called before the first frame update
    void Start()
    {
        etatColis.Add(false);
        etatColis.Add(false);
        StartCoroutine(FaireVenirPremiersColis());
    }

    private void Update()
    {
        if(tempsReponseChangementColis>0)
        {
            tempsReponseChangementColis -= Time.deltaTime;
        }
    }

    public IEnumerator FaireVenirPremiersColis()
    {
        yield return new WaitForSeconds(1f);
        FaireVenirNouveauColis(emplacement);
        emplacement++;
        StartCoroutine(ActiverAutreColis(0));
        yield return new WaitForSeconds(3f);

        FaireVenirNouveauColis(emplacement);
        emplacement = 0;
    }

    public Colis ChoixNouveauColis()
    {
        Colis newColis = new Colis();
        if (colisVider != null)
        {
            if (colisVider.Count > 1)
            {
                int emplacementTempo = Random.Range(0, 3);
                int randomArticleVoulu = 0;
                if (colisActuellementsPose != null && colisActuellementsPose.Count > emplacementTempo)
                {
                    if(colisActuellementsPose[emplacementTempo] != null)
                    {
                        for(int c = 0; c < managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count; c++)
                        {
                            if(!colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[c]) && c!=0)
                            {
                                randomArticleVoulu = c;
                            }
                        }
                    }
                }        
                for (int m = 0; m < colisVider.Count; m++)
                {
                    if (managerColis.colisActuellementTraite[emplacementTempo].listArticles[randomArticleVoulu] == colisVider[m].listArticles[0])
                    {
                        newColis = Instantiate(colisVider[m]);
                    }
                }
            }
            else if (colisVider.Count > 0)
            {
                Debug.Log("Test Fin Creation Colis à vider");
                newColis = Instantiate(colisVider[0]);
            }
            if (Random.Range(0, 100) < chanceColisPasRemplit)
            {
                int nbArticleDebut = newColis.listArticles.Count;
                for (int i = 0; i < nbArticleDebut / 2; i++)
                {
                    newColis.listArticles.RemoveAt(newColis.listArticles.Count);
                }
            }

            if(Random.Range(0,100) < chanceArticlePasBon)
            {
                while (newColis.listArticles[0] == newColis.listArticles[newColis.listArticles.Count-1])
                {
                    newColis.listArticles[newColis.listArticles.Count - 1] = colisVider[Random.Range(0, colisVider.Count - 1)].listArticles[0];
                }
            }
        }
        return newColis;
    }

    public void FaireVenirNouveauColis(int emplacement)
    {
        StartCoroutine(colisAnimationVenir[emplacement].GetComponent<AnimationFaireVenirColis>().AnimationColis(emplacementsScripts[emplacement]));
        if (colisVider != null)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis();
        }
    }


    public bool PeutFairePartirColis()
    {
        if (tempsReponseChangementColis <= 0)
        {
            if (emplacementsScripts[0].activeSelf && emplacementsScripts[1].activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public void FairePartirUnColis()
    {
        /*if (tempsReponseChangementColis <= 0)
        {
            tempsReponseChangementColis = 0.7f;
            if (emplacementsScripts[0].activeSelf && emplacementsScripts[0].activeSelf)
            {*/
        tempsReponseChangementColis = 0.7f;
        etatColis[emplacement] = false;
        for(int p = 1; p < emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Count; p++)
        {
            if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0] != emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[p] && !aEteVerifier)
            {
                Debug.Log("Le colis n'a pas été signalé alors qu'il a une anomalie");
            }
        }
        aEteVerifier = false;
        emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().enabled = false;
        emplacementsScripts[emplacement].SetActive(false);
        StartCoroutine(colisAnimationVenir[emplacement].GetComponent<AnimationFaireVenirColis>().AnimationColisRenvoie(this));
        if (colisVider != null)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis();
        }
        if (emplacementsScripts[(emplacement + 1) % 2] != null)
        {
            StartCoroutine(ActiverAutreColis((emplacement + 1) % 2));
        }
        emplacement = (emplacement + 1) % 2;
        /*}
    }*/
    }

    IEnumerator ActiverAutreColis(int emplacement)
    {
        for(int m = 0; m < 100; m++)
        {
            if (photoArticle.enabled)
            {
                photoArticle.enabled = false;
            }
            photoArticle.sprite = null;
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().enabled = true;
            emplacementsScripts[emplacement].transform.position += new Vector3(0, 1, 0)  * Time.fixedDeltaTime * 0.3f;
            emplacementsScripts[(emplacement + 1) % 2].GetComponent<BoxCollider2D>().enabled = false;
            emplacementsScripts[(emplacement+1)%2].transform.position -= new Vector3(0, 1, 0) * Time.fixedDeltaTime * 0.3f;
            if (m>=99)
            {
                emplacementsScripts[emplacement].GetComponent<BoxCollider2D>().enabled = true;
                //managerColis.AjoutArticleColisVoulu(emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0], Random.Range(0,2), Random.Range(1,7));
                List<int> emplacementsConcerne = new List<int>();
                List<int> nombreArticles = new List<int>();
                for (int i = 0; i < managerColis.colisActuellementTraite.Count; i++)
                {
                    for (int p = 0; p < managerColis.colisActuellementTraite[i].listArticles.Count; p++)
                    {
                        if (managerColis.colisActuellementTraite[i].listArticles[p].rfid == emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0].rfid)
                        {
                            if(!emplacementsConcerne.Contains(i))
                            {
                                emplacementsConcerne.Add(i);
                                nombreArticles.Add(1);
                            }
                            else
                            {
                                for(int q = 0; q < emplacementsConcerne.Count; q++)
                                {
                                    if(emplacementsConcerne[q] == i)
                                    {
                                        nombreArticles[q]++;
                                    }
                                }                          
                            }
                        }
                    }
                }
                managerColis.AjoutArticleColisVoulu(emplacementsConcerne[0], nombreArticles[0]);
                if(!photoArticle.enabled)
                {
                    photoArticle.enabled = true;
                }
                photoArticle.sprite = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0].spriteGTP;
                /* for (int u = 0; u < emplacementsConcerne.Count; u++)
                 {
                     managerColis.AjoutArticleColisVoulu(emplacementsConcerne[u], nombreArticles[u]);
                 }*/
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
