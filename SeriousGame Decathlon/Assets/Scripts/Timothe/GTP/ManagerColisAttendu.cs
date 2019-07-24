using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerColisAttendu : MonoBehaviour
{
    public Monitor monitor;
    public List<Colis> colisVoulus = new List<Colis>();
    public List<Colis> colisActuellementTraite = new List<Colis>();
    public List<int> phasesColisVoulus = new List<int>();
    public ManagerColisVider colisViderManage;
    public List<ConsoleMonitor> cm;

    public int nombreColisVoulu;

    public void Start()
    {
        for(int i = 0; i < nombreColisVoulu; i++)
        {
            colisVoulus.Add(new Colis());
            colisVoulus[i].listArticles = new List<Article>();
            int nb = Random.Range(2, 3);
            int nbPhase = 0;
            for (int k = 0; k < nb; k++)
            {
                int articleAlea = Random.Range(0, colisViderManage.colisVider.Count - 1);
                for (int p = 0; p < Random.Range(2,5); p++)
                {
                    colisVoulus[i].listArticles.Add(colisViderManage.colisVider[articleAlea].listArticles[0]);
                    if(!colisVoulus[i].listArticles.Contains(colisViderManage.colisVider[articleAlea].listArticles[0]))
                    {
                        nbPhase++;
                    }
                }
            }
            phasesColisVoulus.Add(nbPhase);
        }

        for(int q = 0; q < 3; q++)
        {
            colisActuellementTraite.Add(colisVoulus[q]);
            cm[q].phaseActuelle = phasesColisVoulus[q];
        }
    }

    public void RenvoieColis(int emplacement)
    {
        if (colisVoulus.Count > 3 && colisVoulus[3] != null)
        {
            Debug.Log("Test");
            colisVoulus[emplacement] = colisVoulus[3];
            colisVoulus.RemoveAt(3);
            Debug.Log(colisVoulus[3]);
            colisActuellementTraite[emplacement] = colisVoulus[emplacement];
            cm[emplacement].phaseActuelle = phasesColisVoulus[emplacement];
        }
    }

    public void AjoutArticleColisVoulu(int emplacement, int nombreArtVoulu)
    {
        monitor.UpdateAffichage(nombreArtVoulu);
        monitor.ResetMonitor();

        switch(emplacement)
        {
            case 0:
                monitor.Colis1Actif();
                break;
            case 1:
                monitor.Colis2Actif();
                break;
            case 2:
                monitor.Colis3Actif();
                break;
        }
    }

    public bool DetectionAllColis(Colis colisToCompare, int emplacement)
    {
        List<Article> articleEnvoye = new List<Article>();
        List<Article> articleVoulu = new List<Article>();

        foreach(Article art in colisToCompare.listArticles)
        {
            articleEnvoye.Add(art);
        }
        foreach(Article art in colisActuellementTraite[emplacement].listArticles)
        {
            articleVoulu.Add(art);
        }
        //articleEnvoye = colisToCompare.listArticles;
        //articleVoulu = colisVoulus[emplacement].listArticles;

        Debug.Log(articleEnvoye.Count);
        Debug.Log(articleVoulu.Count);

        for (int i = 0; i < articleEnvoye.Count; i++)
        {
            for(int j = 0; j < articleVoulu.Count; j++)
            {
                if (j < 0)
                {
                    j = 0;
                }
                if (i < 0)
                {
                    i = 0;
                }
                Debug.Log("I : " + i + " et J : " + j);
                if (articleEnvoye.Count > 0 && articleVoulu.Count > 0)
                {
                    if (articleEnvoye[i] == articleVoulu[j])
                    {
                        Debug.Log("Test Detect");
                        articleVoulu.RemoveAt(j);
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
        if(colisCompare.listArticles.Count == colisVoulus[emplacement].listArticles.Count)
        {
            for(int i = 0; i < colisCompare.listArticles.Count; i++)
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
}
