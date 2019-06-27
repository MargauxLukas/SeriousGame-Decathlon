using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    SavedData dataSaved;

    //Infos du niveau actuel
    LevelScriptable newLevel;
    private List<Colis> colisNewLevel;

    //Infos du colis actuel
    Colis currentColis;
    Colis colisChoisit;
    private int currentAnomalieNumber;
    public Colis colisDeBase;
    int randomArticle;
    int wrongArticle;

    //Info Iway
    WayTicket newIway;

    //Infos générales
    public List<Article> listArticleBonEtat;
    public List<Article> listArticleSansRFID;
    public List<RefArticle> listRefArticles;
    public List<RFID> listRFIDFonctionnels;
    public List<RFID> listRFIDNonFonctionnels;
    public List<Carton> ListCartons;
    private List<Colis> colisDejaCree;

    // Start is called before the first frame update
    void Awake()
    {
        LoadLevelEditor();
        for(int i = 0; i < dataSaved.nomColisConnus.Count; i++)
        {
            colisDejaCree.Add(SaveLoadSystem.instance.LoadColis(dataSaved.nomColisConnus[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelEditor()
    {
        dataSaved = SaveLoadSystem.instance.LoadGeneralData();
    }

    public void NewLevel()
    {
        newLevel = LevelScriptable.CreateInstance<LevelScriptable>();
        colisNewLevel = new List<Colis>();
        currentAnomalieNumber = 0;
    }

    public void NewColis()
    {
        currentColis = Colis.CreateInstance<Colis>();
        currentAnomalieNumber = 0;
        currentColis = colisDeBase;
        int nbArticleColis = Random.Range(3, 6);
        randomArticle = Random.Range(0, 1);
        wrongArticle = (randomArticle+1) % 2;
        currentColis.PCB = nbArticleColis;
        for(int i = 0; i < nbArticleColis; i++)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
        }
        currentColis.carton = ListCartons[Random.Range(0, 1)];
        currentColis.poids = nbArticleColis * listArticleBonEtat[randomArticle].poids;
        currentColis.fillPercent = 100;
        newIway = WayTicket.CreateInstance<WayTicket>();
        newIway.PCB = currentColis.PCB;
        newIway.refArticle = listRefArticles[randomArticle];
        newIway.numeroCodeBarre = Random.Range(100, 9999999);
        newIway.poids = currentColis.poids;
    }

    public void SaveLevel()
    {
        for(int i = 0; i < colisNewLevel.Count; i ++)
        {
            if(colisNewLevel[i] != null)
            {
                newLevel.colisDuNiveauNoms.Add(colisNewLevel[i].name);
                newLevel.nbColisParNomColis.Add(1);
            }
        }
        SaveLoadSystem.instance.SaveLevel(newLevel, colisNewLevel);
    }

    public void SaveColis()
    {
        //Mise à jour du colis après toutes les modifs
        currentColis.PCB = currentColis.listArticles.Count;
        currentColis.poids = currentColis.PCB * listArticleBonEtat[randomArticle].poids;
        if(currentColis.poids > 20)
        {
            currentColis.fillPercent = 125;
        }
        else if(currentColis.poids <= 5)
        {
            currentColis.fillPercent = 50;
        }
        else
        {
            currentColis.fillPercent = 100;
        }
        currentColis.wayTicket = newIway;

        //Sauvegarde du colis
        SaveLoadSystem.instance.SaveColis(currentColis);
    }

    public void AddColis(int nbColis)
    {
        colisNewLevel.Add(colisDejaCree[nbColis]);
    }

    public void AddAndSaveCurrentColis()
    {
        SaveColis();
        colisNewLevel.Add(currentColis);
    }

    public void RemoveColis(int nbColis)
    {
        colisNewLevel.RemoveAt(nbColis);
    }

    public void OpenMenuMF()
    {

    }

    public void AnomalieChoice(int nbAnomalie)
    {
        if (!currentColis.needQualityControl)
        {
            switch (nbAnomalie)
            {

            }
        }
        currentAnomalieNumber++;
    }

    private void RFIDoverTolerance()
    {
        int rngNb = Random.Range(1, 2);
        for (int i = 0; i <= rngNb; i++)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
        }
    }

    private void RFIDunderToleranceV1()
    {
        currentColis.listArticles[currentColis.listArticles.Count - 1] = listArticleSansRFID[randomArticle];
    }
    private void RFIDunderToleranceV2()
    {
        currentColis.listArticles.RemoveAt(currentColis.listArticles.Count - 1);
    }
    private void DimensionOutToleranceV1()
    {
        currentColis.estAbime = true;
        currentColis.carton = ListCartons[2];
    }
    private void DimensionOutToleranceV2()
    {
        currentColis.estAbime = true;
        currentColis.carton = ListCartons[2];
        int rngNb = Random.Range(1, 3);
        for (int i = 0; i <= rngNb; i++)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
        }
    }
    private void WrongOrientation()
    {
        currentColis.isBadOriented = true;
    }
    private void QualityControl()
    {
        if(currentAnomalieNumber <= 0)
        {
            currentColis.needQualityControl = true;
        }
    }
    private void NewProduct()
    {
        for(int i = 0; i < currentColis.listArticles.Count; i++)
        {
            currentColis.listArticles[i] = listArticleBonEtat[2];
        }
        newIway.refArticle = listRefArticles[2];
    }
    private void RFIDTagToApplied()
    {
        for(int j = 0; j < currentColis.listArticles.Count; j++)
        {
            for(int k = 0; k < listArticleBonEtat.Count; k++)
            {
                if(listArticleBonEtat[k] == currentColis.listArticles[j])
                {
                    currentColis.listArticles[j] = listArticleSansRFID[k];
                }
            }
        }
    }
    private void TooHeavy()
    {
        int nbSecours = 0;
        while (currentColis.poids < 21 && nbSecours < 100)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
            currentColis.poids = currentColis.listArticles.Count * listArticleBonEtat[randomArticle].poids;
        }
    }
    private void RFIDUnexpectedV1()
    {
        currentColis.listArticles[0] = listArticleBonEtat[wrongArticle];
    }

    private void RFIDUnexpectedV2()
    {
        for(int i = 0; i < currentColis.listArticles.Count; i ++)
        {
            currentColis.listArticles[i] = listArticleBonEtat[wrongArticle];
        }
    }

    private void DimensionOutTray()
    {
        currentColis.estAbime = true;
    }
    private void RepackngFP()
    {
        currentColis.estOuvert = true;
    }
}
