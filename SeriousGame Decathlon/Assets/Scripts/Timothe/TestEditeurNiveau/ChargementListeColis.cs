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

    public AnomalieDetection anomDetect;

    public ColisManager manageColis;

    public int nbLevel;
    public int currentLevel;
    public LevelScriptable levelScript;
    public Text affichageNouveauLevel;

    public static ChargementListeColis instance;

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
            Destroy(this);
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

    public void LoadNewLevelScript(int currentLevelTempo)
    {
        List<Colis> newList = new List<Colis>();
        List<Colis> colisListe = new List<Colis>();

        levelScript = LevelScriptable.CreateInstance<LevelScriptable>();
        levelScript = SaveLoadSystem.instance.LoadLevel(currentLevelTempo);

        if (levelScript != null)
        {
            Debug.Log(levelScript);
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
