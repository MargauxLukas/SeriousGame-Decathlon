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
    public List<Colis> colisAvecPack;
    public List<int> positionVoulueParEmplacement;
    private int nbCurrentColis = 0;
    public int chanceColisPasRemplit;
    public int chanceArticlePasBon;
    public int chanceColisPasBon;

    public Image photoArticle;

    public ManagerColisAttendu managerColis;

    public List<RemplissageColisGTP> colisActuellementsPose;

    public GameObject RemainingQuantityWindow;

    public bool aEteVerifier;

    public int emplacement = 0;
    public int AncientEmplacementTempo = -1;

    private float tempsReponseChangementColis;
    int emplacementTempo;

    public Article artReference;

    [Header("Pour le tuto")]
    public List<Colis> colisViderTuto;
    public List<bool> needPack;
    int currentColisNumberTuto;
    public List<int> listPosVoulue;

    // Start is called before the first frame update
    void Start()
    {
        if(ChargementListeColis.instance != null)
        {
            chanceColisPasRemplit = (int)ChargementListeColis.instance.chancePasRemplit;
            chanceArticlePasBon = (int)ChargementListeColis.instance.chanceMauvaisArticle;
            chanceColisPasBon = (int)ChargementListeColis.instance.chanceAllMauvaisArticle;
        }

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

    public Colis ChoixNouveauColis(int empalcementColisCree)
    {
        Colis newColis = new Colis();
        if (colisViderTuto.Count <= 0)
        {
            if (managerColis.colisActuellementTraite[0] != null || managerColis.colisActuellementTraite[1] != null || managerColis.colisActuellementTraite[2] != null)
            {
                if (colisVider != null)
                {
                    if (colisVider.Count > 1)
                    {
                        int emplacementTempo = Random.Range(0, 3);
                        int numberColisNull = 0;
                        if (managerColis.colisActuellementTraite[0] == null)
                        {
                            numberColisNull++;
                        }
                        if (managerColis.colisActuellementTraite[2] == null)
                        {
                            numberColisNull++;
                        }
                        if (managerColis.colisActuellementTraite[1] == null)
                        {
                            numberColisNull++;
                        }
                        if (emplacementTempo == AncientEmplacementTempo && numberColisNull <= 2)
                        {
                            emplacementTempo = (emplacementTempo + 1) % 3;
                            Debug.Log("Test Choix colis déjà traité -1");
                        }
                        int randomArticleVoulu = 0;
                        int nb = 0;
                        while ((managerColis.colisActuellementTraite[emplacementTempo] == null || managerColis.colisActuellementTraite[emplacementTempo].Equals(null)) && nb < 9)
                        {
                            Debug.Log("Test Choix colis déjà traité : " + emplacementTempo);
                            emplacementTempo = (emplacementTempo + 1) % 3;
                            nb++;
                        }
                        if (colisActuellementsPose != null && colisActuellementsPose.Count > emplacementTempo)
                        {
                            Debug.Log("Test Here");
                            randomArticleVoulu = -2;
                            if (managerColis.colisActuellementTraite[emplacementTempo] != null)
                            {
                                for (int c = 0; c < managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count; c++)
                                {
                                    if (randomArticleVoulu == -2)
                                    {
                                        Debug.Log("Test Bug GTP 1 : " + emplacementTempo);
                                        if (colisActuellementsPose[emplacementTempo] == null)
                                        {
                                            Debug.Log("Test Bug GTP 2");
                                            if (emplacementTempo != AncientEmplacementTempo)
                                            {
                                                randomArticleVoulu = 0;
                                            }
                                            else
                                            {
                                                int nbArt = managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count - 1;
                                                for (int s = nbArt; s > 0; s--)
                                                {
                                                    if (managerColis.colisActuellementTraite[emplacementTempo].listArticles[s] != managerColis.colisActuellementTraite[emplacementTempo].listArticles[0])
                                                    {
                                                        randomArticleVoulu = s;
                                                    }
                                                }
                                            }
                                        }
                                        else if (!colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[c]))
                                        {
                                            Debug.Log("Test Bug GTP 3 : " + managerColis.colisActuellementTraite[emplacementTempo].listArticles[c]);
                                            if (emplacementTempo != AncientEmplacementTempo)
                                            {
                                                Debug.Log("Test Bug GTP 4 : " + c);
                                                randomArticleVoulu = c;
                                            }
                                            else
                                            {
                                                if (colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Count > 0)
                                                {
                                                    Debug.Log("Test Bug GTP 5");
                                                    if (!colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[c]))
                                                    {
                                                        Debug.Log("Test Bug GTP 6-Bis : " + c);
                                                        randomArticleVoulu = c;
                                                    }
                                                    else
                                                    {
                                                        for (int g = managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count - 1; g > c - 1; g--)
                                                        {
                                                            if (!colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[g]) && !emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[g]))
                                                            {
                                                                Debug.Log("Test Bug GTP 6 : " + g);
                                                                randomArticleVoulu = g;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    for (int g = c; g < managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count - 1; g++)
                                                    {
                                                        if (!colisActuellementsPose[emplacementTempo].colisScriptable.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[g]) && !emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Contains(managerColis.colisActuellementTraite[emplacementTempo].listArticles[g]))
                                                        {
                                                            Debug.Log("Test Bug GTP 7 : " + g);
                                                            randomArticleVoulu = g;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (c + 1 == managerColis.colisActuellementTraite[emplacementTempo].listArticles.Count)
                                        {
                                            Debug.Log("Test Bug GTP 8");
                                            randomArticleVoulu = 0;
                                        }
                                    }
                                }
                            }
                        }
                        if (randomArticleVoulu >= 0)
                        {
                            //Debug.Log("Passe par ici ?");
                            for (int m = 0; m < colisVider.Count; m++)
                            {
                                int nbSecond = 0;
                                while (managerColis.colisActuellementTraite[emplacementTempo] == null && nbSecond < 9 && emplacementTempo != AncientEmplacementTempo)
                                {
                                    emplacementTempo = (emplacementTempo + 1) % 3;
                                    //Debug.Log("Test Choix colis déjà traité 2 : " + emplacementTempo);
                                    nbSecond++;
                                }
                                if (managerColis.colisActuellementTraite[emplacementTempo].listArticles[randomArticleVoulu] == colisVider[m].listArticles[0])
                                {
                                    //Debug.Log("randomArticleVoulu : " + randomArticleVoulu);
                                    //Debug.Log("Nom du nouvel Article : " + managerColis.colisActuellementTraite[emplacementTempo].listArticles[randomArticleVoulu].name);
                                    //Debug.Log("Le colis choisit en fonction : " + colisVider[m].listArticles[0]);

                                    int nbMemeArticle = 0;
                                    if (colisAvecPack != null && colisAvecPack.Count > 0)
                                    {
                                        foreach (Article art in managerColis.colisActuellementTraite[emplacementTempo].listArticles)
                                        {
                                            if (art == colisVider[m].listArticles[0])
                                            {
                                                nbMemeArticle++;
                                            }
                                        }

                                        if (nbMemeArticle % 3 == 0 && Random.Range(0, 100) < 100 && nbMemeArticle != 0 && colisAvecPack[m] != null)
                                        {
                                            emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack = 3;
                                        }
                                        else
                                        {
                                            emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack = 0;
                                        }
                                    }
                                    else
                                    {
                                        emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack = 0;
                                    }

                                    newColis = Instantiate(colisVider[m]);
                                }
                            }
                        }
                        else
                        {
                            //Debug.Log("Test ici");
                            newColis = null;
                            etatColis[0] = true;
                            etatColis[1] = true;
                        }
                        positionVoulueParEmplacement[empalcementColisCree] = emplacementTempo;
                        AncientEmplacementTempo = emplacementTempo;
                    }
                    else if (colisVider.Count > 0)
                    {
                        newColis = Instantiate(colisVider[0]);
                    }

                    if (newColis != null)
                    {
                        if (Random.Range(0, 100) < chanceColisPasBon)//Mettre un nouveau flaot de chance d'avoir le colis pas bon
                        {
                            emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack = 0;
                            newColis.gtpSupposedToBe = newColis.listArticles[0];
                            Article newArticleMauvais = colisVider[Random.Range(0, colisVider.Count - 1)].listArticles[0];
                            while (newArticleMauvais == newColis.listArticles[0])
                            {
                                newArticleMauvais = colisVider[Random.Range(0, colisVider.Count - 1)].listArticles[0];
                            }
                            for (int m = 0; m < newColis.listArticles.Count; m++)
                            {
                                newColis.listArticles[m] = newArticleMauvais;
                            }
                        }
                        else if (Random.Range(0, 100) < chanceArticlePasBon && emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack <= 0)
                        {
                            while (newColis.listArticles[0] == newColis.listArticles[newColis.listArticles.Count - 1])
                            {
                                newColis.listArticles[newColis.listArticles.Count - 1] = colisVider[Random.Range(0, colisVider.Count - 1)].listArticles[0];
                            }
                        }
                        else if (Random.Range(0, 100) < chanceColisPasRemplit)
                        {
                            int nbArticleDebut = newColis.listArticles.Count;
                            for (int i = 0; i < nbArticleDebut * 2 / 3; i++)
                            {
                                newColis.listArticles.RemoveAt(newColis.listArticles.Count - 1);
                            }
                        }
                    }
                }
            }
            if (newColis != null && newColis.listArticles != null && newColis.listArticles.Count > 0 && newColis.listArticles[0] != null)
            {
                if (newColis.gtpSupposedToBe != null)
                {
                    emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().artReference = newColis.gtpSupposedToBe;
                }
                else
                {
                    emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().artReference = newColis.listArticles[0];
                }
            }
        }
        else
        {
            if (currentColisNumberTuto < colisViderTuto.Count)
            {
                newColis = colisViderTuto[currentColisNumberTuto];
                positionVoulueParEmplacement[empalcementColisCree] = listPosVoulue[currentColisNumberTuto];
                if(needPack[currentColisNumberTuto])
                {
                    emplacementsScripts[empalcementColisCree].GetComponent<AffichagePileArticleGTP>().isFulledWithPack = 3;
                }
                currentColisNumberTuto++;
            }
            else
            {
                newColis = null;
            }
        }
        return newColis;
    }

    public void FaireVenirNouveauColis(int emplacement)
    {
        if (colisVider != null)
        {
            emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis(emplacement);
            if(emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis != null && emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Count <= 4)
            {
                emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().isSupposedToBeEmpty = true;
            }
        }
        if (!managerColis.isLevelEnded)
        {
            StartCoroutine(colisAnimationVenir[emplacement].GetComponent<AnimationFaireVenirColis>().AnimationColis(emplacementsScripts[emplacement]));
        }
    }


    public bool PeutFairePartirColis()
    {
        if (!managerColis.isLevelEnded)
        {
            if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().isSupposedToBeEmpty)
            {
                Debug.Log("Test");
                RemainingQuantityWindow.SetActive(true);
                RemainingQuantityWindow.GetComponent<RemainingQuantityWindow>().articleNb = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Count;
            }
            else if (tempsReponseChangementColis <= 0)
            {
                if ((emplacementsScripts[0].activeSelf && emplacementsScripts[1].activeSelf) || (etatColis[0] && etatColis[1]))
                {
                    if (!emplacementsScripts[0].GetComponent<AffichagePileArticleGTP>().isOpen && !emplacementsScripts[1].GetComponent<AffichagePileArticleGTP>().isOpen && (colisActuellementsPose[0] == null || !colisActuellementsPose[0].isOpen) && (colisActuellementsPose[1] == null || !colisActuellementsPose[1].isOpen) && (colisActuellementsPose[2] == null || !colisActuellementsPose[2].isOpen))
                        return true;
                }
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
                Scoring.instance.LosePointGTP(50, "Tu as oublié de signaler une anomalie sur le SHU");
            }
        }
        if(aEteVerifier)
        {
            if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0] != emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Count-1])
            {
                Scoring.instance.WinPointGTP(70);
            }
            else
            {
                Scoring.instance.LosePointGTP(50, "Tu as signaler une anomalie inexistante sur le SHU");
            }
        }
        aEteVerifier = false;
        emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().enabled = false;
        emplacementsScripts[emplacement].SetActive(false);
        StartCoroutine(colisAnimationVenir[emplacement].GetComponent<AnimationFaireVenirColis>().AnimationColisRenvoie(this));
        if (colisVider != null)
        {
            //emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis();
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
        Scoring.instance.BeginComboGTP(45);

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
                int leBonNombreArticle = 0;
                Article artToCompare = Article.CreateInstance<Article>();
                if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis != null)
                {
                    if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe != null)
                    {
                        artToCompare = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe;
                    }
                    else
                    {
                        artToCompare = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0];
                    }
                }
                if (managerColis.colisActuellementTraite[positionVoulueParEmplacement[emplacement]] != null && emplacementsScripts[(emplacement + 1) % 2].GetComponent<AffichagePileArticleGTP>().currentColis!=null && emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis != null &&emplacementsScripts[(emplacement+1)%2].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe != emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe)
                {
                    for (int x = 0; x < managerColis.colisActuellementTraite[positionVoulueParEmplacement[emplacement]].listArticles.Count; x++)
                    {
                        if (managerColis.colisActuellementTraite[positionVoulueParEmplacement[emplacement]].listArticles[x] == artToCompare)
                        {
                            leBonNombreArticle++;
                        }
                    }
                    managerColis.AjoutArticleColisVoulu(positionVoulueParEmplacement[emplacement], leBonNombreArticle);
                    if (!photoArticle.enabled)
                    {
                        photoArticle.enabled = true;
                    }
                    if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe != null)
                    {
                        photoArticle.sprite = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe.photoGTP;
                    }
                    else
                    {
                        photoArticle.sprite = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0].photoGTP;
                    }
                }
                else
                {
                    Debug.Log("Pass There");
                    emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis = ChoixNouveauColis(emplacement);
                    if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis != null && emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles.Count <= 4)
                    {
                        emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().isSupposedToBeEmpty = true;
                    }
                    else
                    {
                        emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().isSupposedToBeEmpty = false;
                    }
                    if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis != null)
                    {
                        if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe != null)
                        {
                            artToCompare = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe;
                        }
                        else if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0] != null)
                        {
                            artToCompare = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0];
                        }
                    }
                    if (artToCompare != null)
                    {
                        for (int x = 0; x < managerColis.colisActuellementTraite[positionVoulueParEmplacement[emplacement]].listArticles.Count; x++)
                        {
                            if (managerColis.colisActuellementTraite[positionVoulueParEmplacement[emplacement]].listArticles[x] == artToCompare)
                            {
                                leBonNombreArticle++;
                            }
                        }
                        managerColis.AjoutArticleColisVoulu(positionVoulueParEmplacement[emplacement], leBonNombreArticle);
                        if (!photoArticle.enabled)
                        {
                            photoArticle.enabled = true;
                        }
                        if (emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe != null)
                        {
                            photoArticle.sprite = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.gtpSupposedToBe.photoGTP;
                        }
                        else
                        {
                            photoArticle.sprite = emplacementsScripts[emplacement].GetComponent<AffichagePileArticleGTP>().currentColis.listArticles[0].photoGTP;
                        }
                    }
                }
                /* for (int u = 0; u < emplacementsConcerne.Count; u++)
                 {
                     managerColis.AjoutArticleColisVoulu(emplacementsConcerne[u], nombreArticles[u]);
                 }*/
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
