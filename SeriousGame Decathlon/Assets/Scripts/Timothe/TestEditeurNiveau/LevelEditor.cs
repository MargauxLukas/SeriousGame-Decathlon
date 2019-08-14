using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour
{
    private SavedData dataSaved;

    //Infos du niveau actuel
    public LevelScriptable newLevel;
    public List<Colis> colisNewLevel;

    //Infos du colis actuel
    [Header("Info du colis")]
    public Colis currentColis;
    Colis colisChoisit;
    private int currentAnomalieNumber;
    public Colis colisDeBase;
    int randomArticle;
    int wrongArticle;

    //Info Iway
    WayTicket newIway;

    //Infos générales
    [Header("Info à assigner")]
    public List<Article> listArticleBonEtat;
    public List<Article> listArticleSansRFID;
    public List<RefArticle> listRefArticles;
    public List<RFID> listRFIDFonctionnels;
    public List<RFID> listRFIDNonFonctionnels;
    public List<Carton> ListCartons;
    public List<Colis> colisDejaCree;
    public InputField inputFieldColisMF;
    public InputField inputFieldLevel;
    public InputField inputFieldNombreColisRecep;
    public Colis colisToAdd;
    public InputField GtpInputfieldNombreColis;
    public InputField GtpInputfieldMauvaisArticle;
    public InputField GtpInputfieldAllMauvaisArticle;
    public InputField GtpInputfieldColisPeuRemplit;
    public InputField GtpInputfieldColisInternet;
    public InputField GtpInputfieldColisVeutTropArticle;

    //Les Canvas
    [Header("Canvas à Activer/Désactiver")]
    public GameObject creationNiveau;
    public GameObject ongletMultifonction;
    public GameObject ongletReception;
    public GameObject ongletGTP;
    public GameObject ongletAddColis;
    public List<Button> boutonAnomalies;
    public GameObject containerEstDefaillant;


    // Start is called before the first frame update
    void Start()
    {
        LoadLevelEditor();
        colisDejaCree = new List<Colis>();
        if (dataSaved != null && dataSaved.nomColisConnus != null)
        {
            for (int i = 0; i < dataSaved.nomColisConnus.Count; i++)
            {
                colisDejaCree.Add(SaveLoadSystem.instance.LoadColis(dataSaved.nomColisConnus[i]));
            }
        }
    }

    private void Update()
    {
        if (ongletGTP.activeSelf)
        {
            if (!string.IsNullOrEmpty(GtpInputfieldNombreColis.text) && int.Parse(GtpInputfieldNombreColis.text) < 3)
            {
                GtpInputfieldNombreColis.text = "3";
            }

            if (!string.IsNullOrEmpty(GtpInputfieldMauvaisArticle.text) && int.Parse(GtpInputfieldMauvaisArticle.text) > 100)
            {
                GtpInputfieldMauvaisArticle.text = "100";
            }
            else if (!string.IsNullOrEmpty(GtpInputfieldMauvaisArticle.text) && int.Parse(GtpInputfieldMauvaisArticle.text) < 0)
            {
                GtpInputfieldMauvaisArticle.text = "0";
            }

            if (!string.IsNullOrEmpty(GtpInputfieldAllMauvaisArticle.text) && int.Parse(GtpInputfieldAllMauvaisArticle.text) > 100)
            {
                GtpInputfieldAllMauvaisArticle.text = "100";
            }
            else if (!string.IsNullOrEmpty(GtpInputfieldAllMauvaisArticle.text) && int.Parse(GtpInputfieldAllMauvaisArticle.text) < 0)
            {
                GtpInputfieldAllMauvaisArticle.text = "0";
            }

            if (!string.IsNullOrEmpty(GtpInputfieldColisPeuRemplit.text) && int.Parse(GtpInputfieldColisPeuRemplit.text) > 100)
            {
                GtpInputfieldColisPeuRemplit.text = "100";
            }
            else if (!string.IsNullOrEmpty(GtpInputfieldColisPeuRemplit.text) && int.Parse(GtpInputfieldColisPeuRemplit.text) < 0)
            {
                GtpInputfieldColisPeuRemplit.text = "0";
            }

            if (!string.IsNullOrEmpty(GtpInputfieldColisInternet.text) && int.Parse(GtpInputfieldColisInternet.text) > 100)
            {
                GtpInputfieldColisInternet.text = "100";
            }
            else if (!string.IsNullOrEmpty(GtpInputfieldColisInternet.text) && int.Parse(GtpInputfieldColisInternet.text) < 0)
            {
                GtpInputfieldColisInternet.text = "0";
            }

            if (!string.IsNullOrEmpty(GtpInputfieldColisVeutTropArticle.text) && int.Parse(GtpInputfieldColisVeutTropArticle.text) > 100)
            {
                GtpInputfieldColisVeutTropArticle.text = "100";
            }
            else if (!string.IsNullOrEmpty(GtpInputfieldColisVeutTropArticle.text) && int.Parse(GtpInputfieldColisVeutTropArticle.text) < 0)
            {
                GtpInputfieldColisVeutTropArticle.text = "0";
            }
        }
    }
    public void OpenEditor()
    {
        creationNiveau.SetActive(true);
    }

    public void LoadLevelEditor()
    {
        SaveLoadSystem.instance.LoadGeneralData();
        Debug.Log(dataSaved != null);
    }

    public void NewLevel()
    {
        creationNiveau.SetActive(true);
        ongletMultifonction.SetActive(false);

        foreach (Button bouton in boutonAnomalies)
        {
            bouton.interactable = true;
        }

        newLevel = LevelScriptable.CreateInstance<LevelScriptable>();
        colisNewLevel = new List<Colis>();
        currentAnomalieNumber = 0;
    }

    public void SaveLevel()
    {
        if(inputFieldLevel.text != null)
        {
            newLevel.name = inputFieldLevel.text.ToString();
        }

        newLevel.nbColisParNomColis = new List<int>();
        newLevel.colisDuNiveauNoms = new List<string>();

        foreach (Colis colis in colisNewLevel)
        {
            newLevel.nbColisParNomColis.Add(1);
            newLevel.colisDuNiveauNoms.Add(colis.name);
        }

        SaveLoadSystem.instance.SaveLevel(newLevel, colisNewLevel);
    }

    #region GTP

    public void OpenMenuGTP()
    {
        creationNiveau.SetActive(false);
        ongletGTP.SetActive(true);
        newLevel.doesNeedGTP = true;
    }

    public void CloseMenuGTP()
    {
        ongletGTP.SetActive(false);
        creationNiveau.SetActive(true);
    }

    public void ValidateAllField()
    {
        newLevel.nbColisVoulu = int.Parse(GtpInputfieldNombreColis.text);
        newLevel.chanceMauvaisArticle = int.Parse(GtpInputfieldMauvaisArticle.text);
        newLevel.chanceAllMauvaisArticle = int.Parse(GtpInputfieldAllMauvaisArticle.text);
        newLevel.chancePasRemplit = int.Parse(GtpInputfieldColisPeuRemplit.text);
        newLevel.chanceInternet = int.Parse(GtpInputfieldColisInternet.text);
        newLevel.ChanceTropArticle = int.Parse(GtpInputfieldColisVeutTropArticle.text);
    }

    #endregion

    #region Réception

    public void OpenMenuRecep()
    {
        if (newLevel.colisDuNiveauNomReception == null)
        {
            newLevel.colisDuNiveauNomReception = new List<string>();
        }

        if (!newLevel.colisDuNiveauNomReception.Contains(colisToAdd.name))
        {
            newLevel.colisDuNiveauNomReception.Add(colisToAdd.name);
            newLevel.colisDuNiveauNomReception.Add(colisToAdd.name); //Pour augmenter les chances de tomber sur ce colis
            newLevel.colisDuNiveauNomReception.Add(colisToAdd.name); //Pour augmenter encore plus les chances de tomber sur ce colis
        }
        creationNiveau.SetActive(false);
        ongletReception.SetActive(true);
        newLevel.doesNeedRecep = true;
    }

    public void CloseMenuRecep()
    {
        ongletReception.SetActive(false);
        creationNiveau.SetActive(true);
    }

    public void IsContainerDefaillant()
    {
        newLevel.chanceReceptionColisHaveAnomalie += 90;
        if(newLevel.chanceReceptionColisHaveAnomalie > 100)
        {
            newLevel.chanceReceptionColisHaveAnomalie = 10;
            containerEstDefaillant.SetActive(false);
        }
        else
        {
            containerEstDefaillant.SetActive(true);
        }
    }

    public void ValidateNombreColis()
    {
        newLevel.nombreColisReception = int.Parse(inputFieldNombreColisRecep.text);
    }

    public void ChooseColisForReception(Colis colisToAdd)
    {
        if(newLevel.colisDuNiveauNomReception == null)
        {
            newLevel.colisDuNiveauNomReception = new List<string>();
        }

        if(!newLevel.colisDuNiveauNomReception.Contains(colisToAdd.name))
        {
            newLevel.colisDuNiveauNomReception.Add(colisToAdd.name);
            if(colisToAdd.name == "RecepMalOriente")
            {
                newLevel.colisDuNiveauNomReception.Add(colisToAdd.name);
            }
        }
    }

    #endregion

    #region MenuMF

    public void NewColis()
    {
        foreach (Button bouton in boutonAnomalies)
        {
            bouton.interactable = true;
        }

        currentColis = Colis.CreateInstance<Colis>();
        currentAnomalieNumber = 0;
        //currentColis = colisDeBase;
        int nbArticleColis = Random.Range(3, 6);
        randomArticle = Random.Range(0, 3);
        wrongArticle = (randomArticle + 1) % 4;
        currentColis.PCB = nbArticleColis;
        for (int i = 0; i < nbArticleColis; i++)
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

    public void SaveColis()
    {
        if (!colisNewLevel.Contains(currentColis))
        {
            if (inputFieldColisMF.text != null)
            {
                currentColis.name = inputFieldColisMF.text.ToString();
                Debug.Log(currentColis.name);
                //Mise à jour du colis après toutes les modifs
                currentColis.PCB = currentColis.listArticles.Count;
                currentColis.poids = currentColis.PCB * listArticleBonEtat[randomArticle].poids;
                if (currentColis.poids > 20)
                {
                    currentColis.fillPercent = 125;
                }
                else if (currentColis.poids <= 5)
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
            colisNewLevel.Add(currentColis);
        }
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
        creationNiveau.SetActive(false);
        ongletMultifonction.SetActive(true);
        foreach (Button bouton in boutonAnomalies)
        {
            bouton.interactable = false;
        }
        newLevel.doesNeedMF = true;
    }

    public void CloseMenuMF()
    {
        ongletMultifonction.SetActive(false);
        creationNiveau.SetActive(true);
    }

    public void AnomalieChoice(int nbAnomalie)
    {
        if (!currentColis.needQualityControl)
        {
            switch (nbAnomalie)
            {
                case 0:
                    RFIDoverTolerance();
                    break;
                case 1:
                    RFIDunderToleranceV1();
                    break;
                case 2:
                    RFIDunderToleranceV2();
                    break;
                case 3:
                    DimensionOutToleranceV1();
                    break;
                case 4:
                    DimensionOutToleranceV2();
                    break;
                case 5:
                    WrongOrientation();
                    break;
                case 6:
                    QualityControl();
                    break;
                case 7:
                    NewProduct();
                    break;
                case 8:
                    RFIDTagToApplied();
                    break;
                case 9:
                    TooHeavy();
                    break;
                case 10:
                    RFIDUnexpectedV1();
                    break;
                case 11:
                    RFIDUnexpectedV2();
                    break;
                case 12:
                    DimensionOutTray();
                    break;
                case 13:
                    RepackngFP();
                    break;


            }
        }
        currentAnomalieNumber++;

        if(currentAnomalieNumber>=3)
        {
            List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });
            foreach (int nb in buttonToDesactivate)
            {
                boutonAnomalies[nb].interactable = false;
            }
        }
    }

    private void RFIDoverTolerance() //Ajoute entre 1 et 2 articles de la ref de RandomArticle
    {
        int rngNb = Random.Range(1, 2);
        for (int i = 0; i <= rngNb; i++)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
        }
        List<int> buttonToDesactivate = new List<int>(new int[] {0,1,2,4,6,8});
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }

    private void RFIDunderToleranceV1() //Transforme un article en même article sans RFID
    {
        currentColis.listArticles[currentColis.listArticles.Count - 1] = listArticleSansRFID[randomArticle];

        List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2, 4, 6, 8 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void RFIDunderToleranceV2() //Supprime un article à la fin de la liste
    {
        currentColis.listArticles.RemoveAt(currentColis.listArticles.Count - 1);

        List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2, 4, 6, 8 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void DimensionOutToleranceV1() //Met le colis en abimé
    {
        currentColis.estAbime = true;
        currentColis.carton = ListCartons[2];

        List<int> buttonToDesactivate = new List<int>(new int[] { 3, 4, 6 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void DimensionOutToleranceV2() //Met le colis en abimé et lui rajoute des articles de la bonne références (Provoque un RFID Over Tolerance)
    {
        currentColis.estAbime = true;
        currentColis.carton = ListCartons[2];
        int rngNb = Random.Range(1, 3);
        for (int i = 0; i <= rngNb; i++)
        {
            currentColis.listArticles.Add(listArticleBonEtat[randomArticle]);
        }

        List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2, 3, 4, 6 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void WrongOrientation() //Met le colis en bad oriented
    {
        currentColis.isBadOriented = true;

        List<int> buttonToDesactivate = new List<int>(new int[] { 5, 6 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void QualityControl() //Met le colis en NeedControlQuality
    {
        if(currentAnomalieNumber <= 0)
        {
            currentColis.needQualityControl = true;

            List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 });
            foreach (int nb in buttonToDesactivate)
            {
                boutonAnomalies[nb].interactable = false;
            }
        }
    }
    private void NewProduct() //Change tous les articles en articles de nouvelles références
    {
        int rngNewProd = Random.Range(5, 6);
        for(int i = 0; i < currentColis.listArticles.Count; i++)
        {
            currentColis.listArticles[i] = listArticleBonEtat[rngNewProd];
        }
        newIway.refArticle = listRefArticles[rngNewProd];

        List<int> buttonToDesactivate = new List<int>(new int[] { 0, 4, 6, 7, 8, 11 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void RFIDTagToApplied() //Change tous les RFID des articles du colis en RFID non fonctionnel du même article
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

        List<int> buttonToDesactivate = new List<int>(new int[] { 0, 1, 2 , 4 , 6 , 8, 11 , 10 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void TooHeavy() //Rajoute des articles tant que le poids n'est pas > 21
    {
        int nbSecours = 0;
        while (currentColis.poids < 21 && nbSecours < 100)
        {
            currentColis.listArticles.Add(currentColis.listArticles[0]);
            currentColis.poids = currentColis.listArticles.Count * listArticleBonEtat[randomArticle].poids;
        }

        List<int> buttonToDesactivate = new List<int>(new int[] { 6, 9 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void RFIDUnexpectedV1() //Change le premier article du colis en mauvais article
    {
        currentColis.listArticles[1] = listArticleBonEtat[wrongArticle];

        List<int> buttonToDesactivate = new List<int>(new int[] { 6, 8, 10 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }

    private void RFIDUnexpectedV2() //Change tous les articles du colis en mauvais article
    {
        for(int i = 0; i < currentColis.listArticles.Count; i ++)
        {
            currentColis.listArticles[i] = listArticleBonEtat[wrongArticle];
        }

        List<int> buttonToDesactivate = new List<int>(new int[] { 6, 9, 7, 10, 11});
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }

    private void DimensionOutTray() //Met le colis en abimé
    {
        currentColis.estAbime = true;

        currentColis.carton = ListCartons[3];

        List<int> buttonToDesactivate = new List<int>(new int[] { 6, 12 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    private void RepackngFP() //Met le colis en ouvert
    {
        currentColis.estOuvert = true;
        currentColis.estAbime = true;

        List<int> buttonToDesactivate = new List<int>(new int[] { 6, 13 });
        foreach (int nb in buttonToDesactivate)
        {
            boutonAnomalies[nb].interactable = false;
        }
    }
    #endregion
}
