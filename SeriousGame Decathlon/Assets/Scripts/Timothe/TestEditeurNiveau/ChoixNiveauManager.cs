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

    public GameObject contentArea;
    public GameObject button;

    public ChargementListeColis levelLoading;

    public LevelScriptable currentChoiceLevel;
    public int currentLevelNb;

    private SavedData dataSaved;
    public List<Colis> currentColisLevel;

    public AnomalieDetection detect;

    public GameObject affichageAnomalie;
    private List<GameObject> listAffAnomalies;
    public GameObject zoneAffichageAnomalie;

    private List<int> currentAnomalieNumber;
    private List<string> currentAnomalies;


    // Start is called before the first frame update 
    void Start()
    {
        dataSaved = SaveLoadSystem.instance.LoadGeneralData();

        for (int i = 1; i <= dataSaved.nombreNiveauCree; i++)
        {
            Debug.Log("Test");
            GameObject nouveauBouton = Instantiate(button, contentArea.transform);
            nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel = SaveLoadSystem.instance.LoadLevel(i);
            nouveauBouton.GetComponentInChildren<Text>().text = nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel.name;
            nouveauBouton.GetComponent<GetCurrentLevelButton>().nbLevel = i;
            nouveauBouton.GetComponent<GetCurrentLevelButton>().managerLevel = this;
            //currentChoiceLevel = nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel;
        }
        listAffAnomalies = new List<GameObject>();
    }

    public void ShowGeneralInfoLevel(LevelScriptable level)
    {
        currentColisLevel = new List<Colis>();

        currentAnomalieNumber = new List<int>();
        currentAnomalies = new List<string>();

        for(int k = 0; k < 13; k++)
        {
            currentAnomalieNumber.Add(0);
        }

        for(int i = 0; i < level.colisDuNiveauNoms.Count; i++)
        {
            currentColisLevel.Add(SaveLoadSystem.instance.LoadColis(level.colisDuNiveauNoms[i]));
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

        //Instantiate un prefab d'affichage pour chaque anomalie
    }

    public void LoadLevel()
    {
        if (currentLevelNb != 0)
        {
            levelLoading.LoadNewLevelScript(currentLevelNb);
        }
    }
}

