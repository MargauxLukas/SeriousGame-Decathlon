using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoixNiveauManager : MonoBehaviour
{
    public GameObject contentArea;
    public GameObject button;

    public ChargementListeColis levelLoading;

    public LevelScriptable currentChoiceLevel;

    private SavedData dataSaved;
    private List<Colis> currentColisLevel;


    // Start is called before the first frame update
    void Start()
    {
        dataSaved = SaveLoadSystem.instance.LoadGeneralData();
        
        for(int i = 1; i <= dataSaved.nombreNiveauCree; i++)
        {
            Debug.Log("Test");
            GameObject nouveauBouton = Instantiate(button, contentArea.transform);
            nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel = SaveLoadSystem.instance.LoadLevel(i);
            nouveauBouton.GetComponentInChildren<Text>().text = nouveauBouton.GetComponent<GetCurrentLevelButton>().currentLevel.nbLevel.ToString();
            nouveauBouton.GetComponent<GetCurrentLevelButton>().nbLevel = i;
        }
    }

    public void ShowLevelInfo()
    {

    }
}
