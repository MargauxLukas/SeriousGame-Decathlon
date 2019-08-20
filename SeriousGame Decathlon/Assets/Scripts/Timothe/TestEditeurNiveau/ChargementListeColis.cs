using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChargementListeColis : MonoBehaviour
{
    //Info à donner en début de niveau
    /* Les RFID connus du niveau
     */

    public string sceneName;
    public int sceneToLoad;
    public List<int> RFIDKnowed;
    public List<Colis> colisProcessMulti;
    public List<Colis> colisProcessReception;

    public bool needMF = true;
    public bool needGTP = true;
    public bool needRecep = true;

    //Pour le réception
    public float chanceAnomalieRecep;
    public int nombreColisRecep;
    public bool hasBeenReturned;

    //Pour le GTP
    public float nbColisVoulu;
    public float chanceMauvaisArticle;
    public float chanceAllMauvaisArticle;
    public float chancePasRemplit;
    public float chanceInternet;
    public float ChanceTropArticle;

    public int apparitionPos = 0;

    //Général
    public AnomalieDetection anomDetect;

    public ColisManager manageColis;

    public int nbLevel;      //Pas utilisé
    public int currentLevel; //Pas utilisé
    public LevelScriptable levelScript;
    public Text affichageNouveauLevel;

    public Player currentPlayerScriptable;

    public static ChargementListeColis instance;

    public InputField nomDuJoueur;
    public int sceneFinNiveau;

    AsyncOperation async;
    public GameObject loadingScreen;

    public void LoadNewScene(int nbScene)
    {
        if (loadingScreen != null)
        {
            StartCoroutine(LoadNewSceneAsync(nbScene));
        }
        else
        {
            SceneManager.LoadScene(nbScene);
        }
    }

    IEnumerator LoadNewSceneAsync(int nbScene)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(nbScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }


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

        if(nomDuJoueur != null)
        {
            nomDuJoueur.characterLimit = 20;
        }
    }

    private void Start()
    {
        //LoadNewLevelScript(currentLevel);
        currentPlayerScriptable = Instantiate(currentPlayerScriptable);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SortEntrepot()
    {
        LoadNewScene(sceneFinNiveau);
    }

    public void QuitMfLevel(List<Colis> colisMulti, List<int> RFIDknowned)
    {
        colisProcessMulti = colisMulti;
        RFIDKnowed = RFIDknowned;
        apparitionPos = 1;
        if(TutoManagerMulti.instance != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }

    public void QuitReceptionLevel(int nombreColisRestant, bool hasBeenReturn)
    {
        nombreColisRecep = nombreColisRestant;
        apparitionPos = 2;
        if(TutoManagerRecep.instance != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }

    public void QuitGTPLevel(int nombreColisRestant)
    {
        nbColisVoulu = nombreColisRestant;
        apparitionPos = 3;
        SceneManager.LoadScene(6);
    }

    public void LoadNewLevelScript(int currentLevelTempo)
    {
        if (nomDuJoueur != null)
        {
            if (nomDuJoueur.text != null)
            {
                currentPlayerScriptable.name = nomDuJoueur.text;
                Client.instance.SendName(currentPlayerScriptable.name);
            }
            else
            {
                Debug.Log("Test");
                currentPlayerScriptable.name = "Okun Idey";
            }
            currentPlayerScriptable.score = 0;
            currentPlayerScriptable.date = System.DateTime.Now.ToString("dd MMMM yyyy");
        }

        List<Colis> newList = new List<Colis>();
        List<Colis> newListRecep = new List<Colis>();
        List<Colis> colisListe = new List<Colis>();

        levelScript = LevelScriptable.CreateInstance<LevelScriptable>();
        levelScript = ChoixNiveauManager.instance.listLevelScriptable[currentLevelTempo];

        if (levelScript != null)
        {
            //Pour la MF
            if (levelScript.listColisMF != null)
            {
                for (int nb = 0; nb < levelScript.listColisMF.Count; nb++)
                {
                    colisListe.Add(levelScript.listColisMF[nb]);
                    newList.Add(colisListe[nb]);    
                }
                colisProcessMulti = newList;
            }

            //Pour la réception
            nombreColisRecep = levelScript.nombreColisReception;
            chanceAnomalieRecep = levelScript.chanceReceptionColisHaveAnomalie;

            if (levelScript.listColisRecep != null)
            {
                for (int nb = 0; nb < levelScript.colisDuNiveauNomReception.Count; nb++)
                {
                    newListRecep.Add(levelScript.listColisRecep[nb]);
                }

                colisProcessReception = newListRecep;
            }

            //Pour le GTP
            nbColisVoulu = levelScript.nbColisVoulu;
            chanceMauvaisArticle = levelScript.chanceMauvaisArticle;
            chanceAllMauvaisArticle = levelScript.chanceAllMauvaisArticle;
            chancePasRemplit = levelScript.chancePasRemplit;
            chanceInternet = levelScript.chanceInternet;
            ChanceTropArticle = levelScript.ChanceTropArticle;

        }
        //affichageNouveauLevel.text = levelScript.colisDuNiveauNoms[3].ToString();         Ceci est pour le debug

        //Rangement aléatoire des colis
        for(int i = 0; i < newList.Count - 1; i ++)
        {
            int rng = Random.Range(0, newList.Count - 1);
            Colis temporaryColis = newList[rng];
            newList[rng] = newList[i];
            newList[i] = temporaryColis;
        }

        //Ajouter le choix des process pour le level load.
        LoadNewScene(sceneToLoad);
    }

    public void ChangeAuthentificationMessage(string msg)
    {
        Debug.Log(msg);
    }
}
