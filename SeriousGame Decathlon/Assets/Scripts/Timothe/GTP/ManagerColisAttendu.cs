using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColisAttendu : MonoBehaviour
{
    [Header("A Assigner")]
    public Monitor monitor;
    public ManagerColisVider colisViderManage;

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

    public void Start()
    {
        for (int i = 0; i < nombreColisVoulu; i++)
        {
            colisVoulus.Add(new Colis());
            colisVoulus[i].listArticles = new List<Article>();

            int nb = Random.Range(2, 4);
            int nbPhase = 0;

            for (int k = 0; k < nb; k++)
            {
                int articleAlea = Random.Range(0, colisViderManage.colisVider.Count - 1);
                int rngNumber = Random.Range(2, 5);
                for (int p = 0; p < rngNumber; p++)
                {
                    colisVoulus[i].listArticles.Add(colisViderManage.colisVider[articleAlea].listArticles[0]);
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

            if ((float)Random.Range(0, 100) < chanceAvoirTropArticlePrevu)
            {
                while (colisVoulus[i].listArticles.Count <= 11)
                {
                    colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                    colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                }
            }
            phasesColisVoulus.Add(nbPhase); //A changer

            if(Random.Range(0,100)<chanceToComeFromInternet)
            {
                colisVoulus[i].comeFromInternet = true;
            }
        }

        for (int q = 0; q < 3; q++)
        {
            colisActuellementTraite.Add(colisVoulus[q]);
            cm[q].phaseActuelle = phasesColisVoulus[q];
            //Debug.Log("Start les phases : " + phasesColisVoulus[q]);
        }
    }

    public void RenvoieColis(int emplacement)
    {
        if (colisVoulus.Count > 3 && colisVoulus[3] != null)
        {
            //Debug.Log("Test");
            colisVoulus[emplacement] = colisVoulus[3];
            colisVoulus.RemoveAt(3);
            colisActuellementTraite[emplacement] = colisVoulus[emplacement];
            cm[emplacement].phaseActuelle = phasesColisVoulus[emplacement];
            StartCoroutine(colisViderManage.colisActuellementsPose[emplacement].AnimationColisRenvoie());
        }
        else if(colisVoulus.Count<=0)
        {
            StartCoroutine(colisViderManage.colisActuellementsPose[emplacement].AnimationColisRenvoie());
            colisActuellementTraite[emplacement] = null;
            colisVoulus[emplacement] = null;
            //Affichage de la fin du niveau
            ecranFinNiveau.SetActive(true);
        }
        else
        {
            StartCoroutine(colisViderManage.colisActuellementsPose[emplacement].AnimationColisRenvoie());
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

        bool noAnomalie = true;
        if (articleEnvoye.Count > 0 || articleEnvoye.Count > 0)
        {
            noAnomalie = false;
        }
        return noAnomalie;
    }

    public bool DetectionColis(Colis colisCompare, int emplacement)
    {
        if (colisCompare.listArticles.Count == colisVoulus[emplacement].listArticles.Count)
        {
            for (int i = 0; i < colisCompare.listArticles.Count; i++)
            {
                if (colisCompare.listArticles[i].rfid != colisVoulus[emplacement].listArticles[i].rfid)
                {
                    colisVoulus[emplacement] = new Colis();
                    Debug.Log("Un colis a été mal fait");
                    Scoring.instance.LosePointGTP(50, "Il y a un article inatendu dans ton colis");
                    return false;
                    
                }
            }
            colisVoulus[emplacement] = new Colis();
            Scoring.instance.WinPointGTP(150);
            return true;
        }
        colisVoulus[emplacement] = new Colis();
        Scoring.instance.LosePointGTP(50, "Il y a trop ou pas assez d'articles dans ton colis");
        return false;
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
                        articleVoulu .RemoveAt(j);
                        articleEnvoye.RemoveAt(i);
                        j--;
                        i--;
                    }
                }
            }
        }

        Colis colisTempo = Colis.CreateInstance<Colis>();
        int nbPhaseTempo = -1;
        if (colisVoulus.Count >= 4 && colisVoulus[3] != null)
        {
            colisTempo = colisVoulus[3];
            nbPhaseTempo = phasesColisVoulus[3];
        }


        colisRestant.listArticles = articleVoulu;
        Debug.Log(articleVoulu);
        List<Article> artInColis  = new List<Article>();
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
        }

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

        colisVoulus[emplacement] = Instantiate(colisRempli);
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
}