using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChargementListeColis : MonoBehaviour
{
    //Info à donner en début de niveau
    /* Les RFID connus du niveau
     */

    public string sceneName;
    public List<int> RFIDKnowed;
    public List<Colis> colisProcessMulti;

    public AnomalieDetection anomDetect;

    public ColisManager manageColis;

    public int nbLevel;
    public int currentLevel;
    public LevelScriptable levelScript;

    public static ChargementListeColis instance;

    public void LoadNewScene()
    {
        SceneManager.LoadScene(0);
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

    public void LoadNewLevelScript(int currentLevelTempo)
    {
        List<Colis> newList = new List<Colis>();
        List<Colis> colisListe = new List<Colis>();

        levelScript = LevelScriptable.CreateInstance<LevelScriptable>();
        levelScript = SaveLoadSystem.instance.LoadLevel(currentLevelTempo);


        if (levelScript = null)
        {
            Debug.Log("Test Level Null");
            List<string> colisName = new List<string>();
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
        //Trouver un système pour mélanger les colis aléatoirement.
        //Ajouter le choix des process pour le level load.
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            LoadNewLevelScript(currentLevel);
           // LoadNewScene();
        }
    }
}
