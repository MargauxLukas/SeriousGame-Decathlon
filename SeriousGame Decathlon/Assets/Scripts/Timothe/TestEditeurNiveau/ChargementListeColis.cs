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

    //Pour le réception
    public float chanceAnomalieRecep;
    public int nombreColisRecep;

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

    public void LoadNewScene(int nbScene)
    {
        SceneManager.LoadScene(nbScene);
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
    }

    private void Start()
    {
        //LoadNewLevelScript(currentLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SortEntrepot()
    {
        LoadNewScene(sceneFinNiveau);
    }

    public void LoadNewLevelScript(int currentLevelTempo)
    {
        if (nomDuJoueur != null)
        {
            if (nomDuJoueur.text != null)
            {
                currentPlayerScriptable.name = nomDuJoueur.text;
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
        levelScript = SaveLoadSystem.instance.LoadLevel(currentLevelTempo);

        if (levelScript != null)
        {
            //Debug.Log(levelScript);
            if (levelScript.colisDuNiveauNoms != null)
            {
                for (int nb = 0; nb < levelScript.colisDuNiveauNoms.Count; nb++)
                {
                    colisListe.Add(SaveLoadSystem.instance.LoadColis(levelScript.colisDuNiveauNoms[nb]));
                    for (int i = 0; i < levelScript.nbColisParNomColis[nb]; i++)
                    {
                        newList.Add(colisListe[nb]);
                    }
                }
                colisProcessMulti = newList;
            }
            nombreColisRecep = levelScript.nombreColisReception;
            chanceAnomalieRecep = levelScript.chanceReceptionColisHaveAnomalie;

            if (levelScript.colisDuNiveauNomReception != null)
            {
                for (int nb = 0; nb < levelScript.colisDuNiveauNomReception.Count; nb++)
                {
                    newListRecep.Add(SaveLoadSystem.instance.LoadColis(levelScript.colisDuNiveauNomReception[nb]));
                }

                colisProcessReception = newListRecep;
            }
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
}
