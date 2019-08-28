using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerColisAttendu : MonoBehaviour
{
    [Header("A Assigner")]
    public Monitor monitor;
    public ManagerColisVider colisViderManager;

    [Header("Liste")]
    public List<Colis> colisVoulus             = new List<Colis>(); //Tous les colis voulus
    public List<Colis> colisActuellementTraite = new List<Colis>(); //Tous les colis actuel
    public List<int  > phasesColisVoulus       = new List<int  >();
    public List<ConsoleMonitor> cm;

    [Header("INT")]
    public int nombreColisVoulu;
    public int nbArticleVoulu  ;
    public int nbEmplacement   ;

    public float chanceAvoirTropArticlePrevu = 0;
    public float chanceToComeFromInternet    = 0;

    public GameObject ecranFinNiveau;

    private int decompteFinNiveau;
    private int nbColisCree;

    public bool isLevelEnded = false;
    public GameObject loadingScreen;

    public GameObject desactiveEnQuittant;

    public void Start()
    {
        if(ChargementListeColis.instance != null)
        {
            ChargementListeColis.instance.loadingScreen = loadingScreen;
            nombreColisVoulu = (int)ChargementListeColis.instance.nbColisVoulu;
            chanceAvoirTropArticlePrevu = (int)ChargementListeColis.instance.ChanceTropArticle;
            chanceToComeFromInternet = (int)ChargementListeColis.instance.chanceInternet;
        }

        for (int i = 0; i < nombreColisVoulu; i++)
        {
            colisVoulus.Add(new Colis());
            colisVoulus[i].listArticles = new List<Article>();

            int nb = Random.Range(2, 4);
            int nbPhase = 0;

            for (int k = 0; k < nb; k++)
            {
                int articleAlea = Random.Range(0, colisViderManager.colisVider.Count - 1);
                int rngNumber = Random.Range(2, 5);
                if (colisVoulus[i].listArticles.Count > 7)
                {
                    if (10 - colisVoulus[i].listArticles.Count > 2)
                    {
                        rngNumber = Random.Range(2, 10 - colisVoulus[i].listArticles.Count);
                    }
                    else
                    {
                        rngNumber = 2;
                    }
                }
                for (int p = 0; p < rngNumber; p++)
                {
                    colisVoulus[i].listArticles.Add(colisViderManager.colisVider[articleAlea].listArticles[0]);
                }
            }

            List<Article> articleDejaConnus = new List<Article>();
            foreach(Article art in colisVoulus[i].listArticles)
            {
                if(!articleDejaConnus.Contains(art))
                {
                    articleDejaConnus.Add(art);
                    nbPhase++;
                }
            }

            if ((float)Random.Range(0, nombreColisVoulu - nbColisCree) < chanceAvoirTropArticlePrevu)
            {
                int f = 0;
                int rng = Random.Range(0, colisViderManager.colisVider.Count);

                int rngNbArt = Random.Range(10, 13);
                chanceAvoirTropArticlePrevu--;
                List<int> nbMemeArticleTempo = new List<int>();
                List<Article> articleConnuTempo = new List<Article>();
                for (int m = 0; m < colisVoulus[i].listArticles.Count; m++)
                {
                    if(!articleConnuTempo.Contains(colisVoulus[i].listArticles[m]))
                    {
                        articleConnuTempo.Add(colisVoulus[i].listArticles[m]);
                        nbMemeArticleTempo.Add(1);
                    }
                    else
                    {
                        nbMemeArticleTempo[nbMemeArticleTempo.Count - 1]++;
                    }
                }
                int nbArtRng = 0;
                while (colisVoulus[i].listArticles.Count <= rngNbArt)
                {
                    if (nbPhase > 1)
                    {
                        int nbArt = colisVoulus[i].listArticles.Count - 1;
                        while(nbArt>0 && colisVoulus[i].listArticles[nbArt] == colisVoulus[i].listArticles[0])
                        {
                            nbArt--;
                        }
                        if (nbMemeArticleTempo[0] < 9)
                        {
                            colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                            nbMemeArticleTempo[0]++;
                        }
                        if (nbMemeArticleTempo[nbMemeArticleTempo.Count - 1] < 9)
                        {
                            colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[nbArt]);
                            nbMemeArticleTempo[nbMemeArticleTempo.Count - 1]++;
                        }
                    }
                    else
                    {
                        while (f < 10 && colisViderManager.colisVider[rng].listArticles[0] == colisVoulus[i].listArticles[0])
                        {
                            rng = (rng + 1) % colisViderManager.colisVider.Count;
                            f++;
                        }
                        
                        nbPhase = 2;
                        if (nbMemeArticleTempo[0] < 9)
                        {
                            colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                            nbMemeArticleTempo[0]++;
                        }
                        if (nbArtRng < 9)
                        {
                            colisVoulus[i].listArticles.Add(colisViderManager.colisVider[rng].listArticles[0]);
                            nbArtRng++;
                        }
                    }
                }
            }

            List<int> nbMemeArticle = new List<int>();
            List<Article> articleConnu = new List<Article>();

            //Pour le Debug
            Debug.Log("Colis n°" + i);
            for (int m = 0; m < colisVoulus[i].listArticles.Count; m++)
            {
                if (!articleConnu.Contains(colisVoulus[i].listArticles[m]))
                {
                    articleConnu.Add(colisVoulus[i].listArticles[m]);
                    nbMemeArticle.Add(1);
                }
                else
                {
                    nbMemeArticle[nbMemeArticle.Count - 1]++;
                }
            }
            for (int m = 0; m < nbMemeArticle.Count; m++)
            {
                Debug.Log("Nombre de meme article : " + nbMemeArticle[m]);
            }

            nbPhase = 0;
            articleDejaConnus = new List<Article>();
            foreach (Article art in colisVoulus[i].listArticles)
            {
                if (!articleDejaConnus.Contains(art))
                {
                    articleDejaConnus.Add(art);
                    nbPhase++;
                }
            }

            phasesColisVoulus.Add(nbPhase);

            if(Random.Range(0, nombreColisVoulu-nbColisCree) < chanceToComeFromInternet)
            {
                colisVoulus[i].comeFromInternet = true;
                chanceToComeFromInternet--;
            }
            nbColisCree++;
        }

        for (int q = 0; q < 3; q++)
        {
            colisActuellementTraite.Add(colisVoulus[q]);
            cm[q].phaseActuelle = phasesColisVoulus[q];
            //Debug.Log("Start les phases : " + phasesColisVoulus[q]);
        }

        for(int tutoColisSourceNum = 0; tutoColisSourceNum < colisViderManager.colisViderTuto.Count; tutoColisSourceNum++)
        {
            Debug.Log(colisViderManager.colisViderTuto[tutoColisSourceNum]);
            Debug.Log(Instantiate(colisViderManager.colisViderTuto[tutoColisSourceNum]));
            colisViderManager.colisViderTuto[tutoColisSourceNum] = Instantiate(colisViderManager.colisViderTuto[tutoColisSourceNum]);
        }

        for (int tutoCommandeNum = 0; tutoCommandeNum < colisVoulus.Count; tutoCommandeNum++)
        {
            Debug.Log(colisVoulus[tutoCommandeNum]);
            Debug.Log(Instantiate(colisVoulus[tutoCommandeNum]));
            colisVoulus[tutoCommandeNum] = Instantiate(colisVoulus[tutoCommandeNum]);
        }
    }

    public void RenvoieColis(int emplacement)
    {
        monitor.ResetMonitor();
        if (colisVoulus.Count > 3 && colisVoulus[3] != null)
        {
            colisVoulus[emplacement] = colisVoulus[3];
            colisVoulus.RemoveAt(3);
            phasesColisVoulus[emplacement] = phasesColisVoulus[3];
            phasesColisVoulus.RemoveAt(3);
            colisActuellementTraite[emplacement] = colisVoulus[emplacement];
            cm[emplacement].phaseActuelle = phasesColisVoulus[emplacement];
            StartCoroutine(colisViderManager.colisActuellementsPose[emplacement].AnimationColisRenvoie());
        }
        else if(decompteFinNiveau>=2)
        {
            Debug.Log("Fin de niveau");
            isLevelEnded = true;
            ecranFinNiveau.SetActive(true);
            StartCoroutine(colisViderManager.colisActuellementsPose[emplacement].AnimationColisRenvoie());
            colisActuellementTraite[emplacement] = null;
            colisVoulus[emplacement] = null;
            //Affichage de la fin du niveau
        }
        else
        {
            decompteFinNiveau++;
            Debug.Log("Allo");
            StartCoroutine(colisViderManager.colisActuellementsPose[emplacement].AnimationColisRenvoie());
            colisActuellementTraite[emplacement] = null;
            colisVoulus[emplacement] = null;
        }
    }

    public void AjoutArticleColisVoulu(int emplacement, int nombreArtVoulu)
    {
        nbArticleVoulu = nombreArtVoulu;
        monitor.UpdateAffichage(nombreArtVoulu);
        monitor.ResetMonitor();

        switch (emplacement)
        {
            case 0:
                nbEmplacement = 0;
                if (cm[0].colisActuelPoste == null){monitor.Colis1Actif(phasesColisVoulus[0],    0                               , colisActuellementTraite[0].comeFromInternet);}
                else                               {monitor.Colis1Actif(phasesColisVoulus[0], cm[0].colisActuelPoste.currentPhase, colisActuellementTraite[0].comeFromInternet);}
                break;
            case 1:
                nbEmplacement = 1;
                if (cm[1].colisActuelPoste == null){monitor.Colis2Actif(phasesColisVoulus[1],    0                               , colisActuellementTraite[1].comeFromInternet);}
                else                               {monitor.Colis2Actif(phasesColisVoulus[1], cm[1].colisActuelPoste.currentPhase, colisActuellementTraite[1].comeFromInternet);}
                break;
            case 2:
                nbEmplacement = 2;
                if (cm[2].colisActuelPoste == null){monitor.Colis3Actif(phasesColisVoulus[2],    0                               , colisActuellementTraite[2].comeFromInternet);}
                else                               {monitor.Colis3Actif(phasesColisVoulus[2], cm[2].colisActuelPoste.currentPhase, colisActuellementTraite[2].comeFromInternet);}
                break;
        }
    }

    public bool DetectionAllColis(Colis colisToCompare, int emplacement)
    {
        List<Article> articleEnvoye = new List<Article>();
        List<Article> articleVoulu  = new List<Article>();

        foreach (Article art in colisToCompare.listArticles)
        {
            articleEnvoye.Add(art);
        }
        foreach (Article art in colisActuellementTraite[emplacement].listArticles)
        {
            articleVoulu.Add(art);
        }
        //articleEnvoye = colisToCompare.listArticles;
        //articleVoulu = colisVoulus[emplacement].listArticles;

        Debug.Log(articleEnvoye.Count);
        Debug.Log(articleVoulu .Count);

        for (int i = 0; i < articleEnvoye.Count; i++)
        {
            for (int j = 0; j < articleVoulu.Count; j++)
            {
                if (j < 0){j = 0;}
                if (i < 0){i = 0;}
                Debug.Log("I : " + i + " et J : " + j);
                if (articleEnvoye.Count > 0 && articleVoulu.Count > 0)
                {
                    if (articleEnvoye[i] == articleVoulu[j])
                    {
                        Debug.Log("Test Detect");
                        articleVoulu .RemoveAt(j);
                        articleEnvoye.RemoveAt(i);
                        j--;
                        i--;
                    }
                }
            }
        }

        if (colisActuellementTraite[emplacement].comeFromInternet)
        {
            Debug.Log("Test ici");
            if (colisViderManager.emplacementsScripts[emplacement].GetComponent<RemplissageColisGTP>().nbArticleScanned != colisToCompare.listArticles.Count)
            {
                Debug.Log("Et là");
                Scoring.instance.LosePointGTP(50, "Articles Internet non scannés");
            }
        }

        bool noAnomalie = true;
        if (articleEnvoye.Count > 0 || articleEnvoye.Count > 0)
        {
            noAnomalie = false;
        }
        return noAnomalie;
    }

    public bool DetectionColis(Colis colisCompare, int emplacement)
    {
        if (colisVoulus[emplacement].listArticles.Count > 0)
        {
            List<Article> articleEnvoye = new List<Article>();
            List<Article> articleVoulu = new List<Article>();

            foreach (Article art in colisCompare.listArticles)
            {
                articleEnvoye.Add(art);
            }
            foreach (Article art in colisVoulus[emplacement].listArticles)
            {
                articleVoulu.Add(art);
            }
            for (int i = 0; i < articleEnvoye.Count; i++)
            {
                for (int j = 0; j < articleVoulu.Count; j++)
                {
                    if (j < 0) { j = 0; }
                    if (i < 0) { i = 0; }
                    //Debug.Log("I : " + i + " et J : " + j);
                    if (articleEnvoye.Count > 0 && articleVoulu.Count > 0)
                    {
                        if (articleEnvoye[i] == articleVoulu[j])
                        {
                            //Debug.Log("Test Detect");
                            articleVoulu.RemoveAt(j);
                            articleEnvoye.RemoveAt(i);
                            j--;
                            i--;
                        }
                    }
                }
            }

            if (colisActuellementTraite[emplacement].comeFromInternet)
            {
                Debug.Log("Articles scanné " + colisViderManager.colisActuellementsPose[emplacement].GetComponent<RemplissageColisGTP>().nbArticleScanned);
                Debug.Log("Nombre Articles " + colisCompare.listArticles.Count);
                if (colisViderManager.colisActuellementsPose[emplacement].GetComponent<RemplissageColisGTP>().nbArticleScanned != colisCompare.listArticles.Count)
                {
                    Scoring.instance.LosePointGTP(50, "Articles Internet non scannés");
                }
            }

            if (articleEnvoye.Count>0 || articleVoulu.Count>0)
            {
                colisVoulus[emplacement] = new Colis();
                Debug.Log("Un colis a été mal fait");
                if (articleEnvoye.Count == 0 || articleVoulu.Count == 0)
                {
                    Scoring.instance.LosePointGTP(50, "Articles en trop/manquants dans le colis");
                }
                else
                {
                    Scoring.instance.LosePointGTP(50, "Article inattendu dans le colis");
                }
                return false;
            }
            else
            {
                colisVoulus[emplacement] = new Colis();
                Scoring.instance.WinPointGTP(150);
                return true;
            }
        }
        return true;
    }

    public void ClosePickTU(int emplacement, Colis colisRempli, RemplissageColisGTP colisScript)
    {
        if(colisRempli.listArticles.Count >=9)
        {
            Scoring.instance.WinPointGTP(70);
        }

        Colis colisRestant = Colis.CreateInstance<Colis>();

        List<Article> articleEnvoye = new List<Article>();
        List<Article> articleVoulu  = new List<Article>();

        foreach (Article art in colisRempli.listArticles)
        {
            articleEnvoye.Add(art);
        }
        foreach (Article art in colisVoulus[emplacement].listArticles)
        {
            articleVoulu.Add(art);
        }
        for (int i = 0; i < articleEnvoye.Count; i++)
        {
            for (int j = 0; j < articleVoulu.Count; j++)
            {
                if (j < 0){j = 0;}
                if (i < 0){i = 0;}
                //Debug.Log("I : " + i + " et J : " + j);
                if (articleEnvoye.Count > 0 && articleVoulu.Count > 0)
                {
                    if (articleEnvoye[i] == articleVoulu[j])
                    {
                        //Debug.Log("Test Detect");
                        colisRestant.listArticles.Add(articleVoulu[j]);
                        articleVoulu .RemoveAt(j);
                        articleEnvoye.RemoveAt(i);
                        j--;
                        i--;
                    }
                }
            }
        }
        int tailleListVoulue = colisRestant.listArticles.Count;
        for(int n = 0; n < tailleListVoulue; n++)
        {
            for(int b = 0; b < articleVoulu.Count; b++)
            {
                if(articleVoulu[b] == colisRestant.listArticles[n])
                {
                    colisRestant.listArticles.Add(articleVoulu[b]);
                    articleVoulu.RemoveAt(b);
                    b--;
                }
            }
        }

        Debug.Log(articleVoulu);
        /*List<Article> artInColis  = new List<Article>();
        List <int> nbArticleIdentique = new List<int>();
        Article previousArticle = new Article();

        foreach(Article art in colisRestant.listArticles)
        {
            if(art == previousArticle)
            {
                nbArticleIdentique[nbArticleIdentique.Count - 1]++;
            }
            if(!artInColis.Contains(art))
            {
                artInColis.Add(art);
                nbArticleIdentique.Add(1);
            }
            previousArticle = art;
        }*/

        /*if (colisRestant.listArticles.Count > 0)
        {
            if (colisVoulus.Count >= 4 && colisVoulus[3] != null)
            {
                phasesColisVoulus[3] = artInColis.Count - 1;
                colisVoulus[3] = colisRestant;
            }
            else
            {
                phasesColisVoulus.Add(artInColis.Count - 1);
                colisVoulus.Add(colisRestant);
            }
        }*/
        if (colisRestant.listArticles.Count > 0)
        {
            colisVoulus[emplacement] = Instantiate(colisRestant);
            Debug.Log("Changement de colis");
        }
        phasesColisVoulus[emplacement] = 0;
        colisScript.currentPhase = 12;
        /*if (nbPhaseTempo >= 0)
        {
            phasesColisVoulus.Add(nbPhaseTempo);
            colisVoulus.Add(colisTempo);
        }*/
    }

    public void CorrectPickQuantity(int emplacement, Colis colisRempli, int nombreArticleVoulu, Article articleEnQuestion)
    {
        int nbArticleTotal = 0;
        foreach(Article art in colisVoulus[emplacement].listArticles)
        {
            if(art == articleEnQuestion)
            {
                nbArticleTotal++;
            }
        }

        //ClosePickTU(emplacement, colisRempli);

        for(int i = 0; i < colisVoulus[emplacement].listArticles.Count; i++)
        {
            if(i<0){i++;}

            if (colisVoulus[emplacement].listArticles[i] == articleEnQuestion)
            {
                colisVoulus[emplacement].listArticles.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < colisVoulus[3].listArticles.Count; i++)
        {
            if (i < 0){i++;}

            if (colisVoulus[3].listArticles[i] == articleEnQuestion)
            {
                colisVoulus[3].listArticles.RemoveAt(i);
                i--;
            }
        }

        for(int l = 0; l < nombreArticleVoulu; l ++)
        {
            colisVoulus[emplacement].listArticles.Add(articleEnQuestion);
        }

        for(int m = 0; m < nbArticleTotal - nombreArticleVoulu; m++)
        {
            colisVoulus[3].listArticles.Add(articleEnQuestion);
        }
    }

    public void Quit()
    {
        if (ChargementListeColis.instance == null)
        {
            desactiveEnQuittant.SetActive(false);
            SceneManager.LoadScene(6);
        }
        else
        {
            if (colisVoulus.Count > 3)
            {
                desactiveEnQuittant.SetActive(false);
                ChargementListeColis.instance.QuitGTPLevel(colisVoulus.Count);
            }
            else if (isLevelEnded)
            {
                desactiveEnQuittant.SetActive(false);
                ChargementListeColis.instance.QuitGTPLevel(0);
            }
            else
            {
                desactiveEnQuittant.SetActive(false);
                ChargementListeColis.instance.QuitGTPLevel(3);
            }
        }
    }
}