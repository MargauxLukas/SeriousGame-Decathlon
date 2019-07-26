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

    float chanceAvoirTropArticlePrevu = 0;

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
                nbPhase++;
                int articleAlea = Random.Range(0, colisViderManage.colisVider.Count - 1);
                int rngNumber = Random.Range(2, 5);
                for (int p = 0; p < rngNumber; p++)
                {
                    colisVoulus[i].listArticles.Add(colisViderManage.colisVider[articleAlea].listArticles[0]);
                }
            }

            if ((float)Random.Range(0, 1) < chanceAvoirTropArticlePrevu)
            {
                while (colisVoulus[i].listArticles.Count <= 11)
                {
                    colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                    colisVoulus[i].listArticles.Add(colisVoulus[i].listArticles[0]);
                }
            }
            phasesColisVoulus.Add(nbPhase);
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
            Debug.Log(colisVoulus[3]);
            colisActuellementTraite[emplacement] = colisVoulus[emplacement];
            cm[emplacement].phaseActuelle = phasesColisVoulus[emplacement];
            StartCoroutine(colisViderManage.colisActuellementsPose[emplacement].AnimationColisRenvoie());
        }
        else if(colisVoulus.Count<=0)
        {
            //Affichage de la fin du niveau
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
                monitor.Colis1Actif(phasesColisVoulus[0], cm[0].phaseActuelle);
                break;
            case 1:
                nbEmplacement = 1;
                monitor.Colis2Actif(phasesColisVoulus[1], cm[0].phaseActuelle);
                break;
            case 2:
                nbEmplacement = 2;
                monitor.Colis3Actif(phasesColisVoulus[2], cm[0].phaseActuelle);
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
            //Le colis renvoyé n'est pas bon
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
                    return false;
                }
            }
            colisVoulus[emplacement] = new Colis();
            return true;
        }
        colisVoulus[emplacement] = new Colis();
        return false;
    }

    public void ClosePickTU(int emplacement, Colis colisRempli)
    {
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

        Colis colisTempo = colisVoulus[3];
        int nbPhaseTempo = phasesColisVoulus[3];

        colisRestant.listArticles = articleVoulu;
        List<Article> artInColis  = new List<Article>();

        foreach(Article art in colisRestant.listArticles)
        {
            if(!artInColis.Contains(art))
            {
                artInColis.Add(art);
            }
        }

        phasesColisVoulus[3] = artInColis.Count - 1;
        colisVoulus[3] = colisRestant;

        colisVoulus[emplacement] = Instantiate(colisRempli);
        phasesColisVoulus[emplacement] = 0;

        phasesColisVoulus.Add(nbPhaseTempo);
        colisVoulus      .Add(colisTempo  );
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

        ClosePickTU(emplacement, colisRempli);

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