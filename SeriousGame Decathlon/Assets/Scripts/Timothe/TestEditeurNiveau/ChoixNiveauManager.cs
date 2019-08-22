using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixNiveauManager : MonoBehaviour
{
    /* Reste à faire :
     *  Afficher le nombre des anomalies présentes
     *  Mettre le bouton pour changer de scène avec le bon niveau
     */
    public static ChoixNiveauManager instance;

    public GameObject contentArea;
    public GameObject button;

    public AudioSource source;
    public AudioClip musique;
    public float coefSound = 0.4f;

    public ChargementListeColis levelLoading;

    public LevelScriptable currentChoiceLevel;
    public int currentLevelNb;
    public List<Colis> currentColisLevel;

    public AnomalieDetection detect;

    //Boutons d'affichage
    public Button boutonsAffichageMF;
    public Button boutonsAffichageReception;
    public Button boutonsAffichageGTP;

    //Multifoncition

    public GameObject affichageAnomalie;
    private List<GameObject> listAffAnomalies;
    public GameObject zoneAffichageAnomalie;

    private List<int> currentAnomalieNumber;
    private List<string> currentAnomalies;

    public LevelScriptable[] listLevelScriptable = new LevelScriptable[100];

    //Réception
    public Text affichageNombreColisRecep;

    //GTP
    public Text affichageNombreColisGtp;

    private RectTransform rt;

    //WIFI
    public GameObject Wifi;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        if (Wifi != null)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Wifi.SetActive(true);
            }
            else
            {
                Wifi.SetActive(false);
                SaveLoadSystem.instance.LoadGeneralData("GeneralData");
                rt = contentArea.GetComponent(typeof(RectTransform)) as RectTransform;
                listAffAnomalies = new List<GameObject>();
            }
        }
    }

    public void affichageLevel(string json, int i)
    {
        Debug.Log("Arrive ici 1");
        
        LevelScriptable levelScript = SaveLoadSystem.instance.GetLevel(json);

        listLevelScriptable[i] = levelScript;

        GameObject nouveauBouton = Instantiate(button, contentArea.transform);
        nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel = levelScript;
        nouveauBouton.GetComponentInChildren<Text>().text = nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel.name;
        nouveauBouton.GetComponent<GetCurrentLevelButton>().nbLevel = i;
        nouveauBouton.GetComponent<GetCurrentLevelButton>().managerLevel = this;
        rt.sizeDelta += new Vector2(0, 130);
    }

    public void SelectLevelMF(string fileColis, string WayTicket, int nbLevel)
    {
        Colis colis = SaveLoadSystem.instance.LoadColis(fileColis, WayTicket);
        listLevelScriptable[nbLevel].AddColis(colis);
        listLevelScriptable[nbLevel].AddColisMF(colis);
    }

    public void SelectLevelRecep(string fileColis, string WayTicket, int nbLevel)
    {
        Colis colis = SaveLoadSystem.instance.LoadColis(fileColis, WayTicket);
        listLevelScriptable[nbLevel].AddColis(colis);
        listLevelScriptable[nbLevel].AddColisRecep(colis);
    }

    public void ShowGeneralInfoLevel(LevelScriptable level)
    {
        boutonsAffichageGTP.interactable = false;
        boutonsAffichageMF.interactable = false;
        boutonsAffichageReception.interactable = false;

        if (level.doesNeedMF && level.colisDuNiveauNoms != null && level.colisDuNiveauNoms.Count>0)
        {
            boutonsAffichageMF.interactable = true;
        }
        if(level.doesNeedRecep && level.nombreColisReception > 0)
        {
            boutonsAffichageReception.interactable = true;
        }
        if(level.doesNeedGTP && level.nbColisVoulu > 3)
        {
            boutonsAffichageGTP.interactable = true;
        }

        currentColisLevel = new List<Colis>();
        currentColisLevel = level.listColis;

        currentAnomalieNumber = new List<int>();
        currentAnomalies = new List<string>();

        for(int k = 0; k < 13; k++)
        {
            currentAnomalieNumber.Add(0);
        }

        for(int i = 0; i < level.colisDuNiveauNoms.Count; i++)
        {
            detect.CheckColis(currentColisLevel[i]);

            for(int j = 0; j < currentColisLevel[i].nbAnomalie; j++)
            {
                if(!currentAnomalies.Contains(currentColisLevel[i].listAnomalies[j]))
                {
                    currentAnomalies.Add(currentColisLevel[i].listAnomalies[j]);
                }

                for (int m = 0; m < currentAnomalies.Count; m++)
                {
                    if (currentColisLevel[i].listAnomalies[j] == currentAnomalies[m])
                    {
                        currentAnomalieNumber[m]++;
                    }
                }
            }
        }

        if(listAffAnomalies.Count>0)
        {
            foreach(GameObject gm in listAffAnomalies)
            {
                Destroy(gm);
            }
            listAffAnomalies = new List<GameObject>();
        }

        for(int p = 0; p < currentAnomalies.Count; p++)
        {
            listAffAnomalies.Add(Instantiate(affichageAnomalie, zoneAffichageAnomalie.transform));
            listAffAnomalies[p].GetComponent<Text>().text = currentAnomalies[p];
        }

        affichageNombreColisRecep.text = level.nombreColisReception.ToString();
        affichageNombreColisGtp.text = level.nbColisVoulu.ToString();

        //Instantiate un prefab d'affichage pour chaque anomalie
    }

    public void LoadLevel()
    {
        if (currentLevelNb != 0)
        {
            if (musique != null && source != null)
            {
                source.clip = musique;
                source.volume = coefSound;
                source.Play();
            }
            levelLoading.LoadNewLevelScript(currentLevelNb);
        }
    }

    public void DeleteLevel()
    {
        currentChoiceLevel = SaveLoadSystem.instance.LoadLevel(currentLevelNb);
        //Et à partir de là, tu peux le suppr normalement...
    }
}

