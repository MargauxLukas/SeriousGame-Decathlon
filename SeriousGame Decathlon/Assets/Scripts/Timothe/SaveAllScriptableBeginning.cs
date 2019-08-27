using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAllScriptableBeginning : MonoBehaviour
{
    public static SaveAllScriptableBeginning instance { private set; get; }

    public List<Colis> allColisCreated;
    public List<LevelScriptable> allLevelCreated;
    public BestScoreScript beginScore;
    public int currentVersion;

    public bool isSaveFile       = false;
    public SavedData newData;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }   
    }

    public void StartAll(bool isSaveFile, SavedData newData = null)
    {
        if (!isSaveFile)
        {
            foreach (Colis coli in allColisCreated)
            {
                SaveLoadSystem.instance.SaveColis(coli);
            }

            foreach (LevelScriptable level in allLevelCreated)
            {
                SaveLoadSystem.instance.SaveLevelWithoutColis(level);
            }

            SaveLoadSystem.instance.SaveBestBegin(beginScore);
        }
    }

    int dellNumber = 0;
    public SpriteRenderer feedbackSuppression;

    public void DesinstallFirst()
    {
        if(dellNumber==0)
        {
            dellNumber = 1;
            StartCoroutine(FeedbackDeleteFonction());
        }
    }
    public void DesinstallSecond()
    {
        if (dellNumber == 1)
        {
            dellNumber = 2;
            StartCoroutine(FeedbackDeleteFonction());
        }
    }
    public void DesinstallThird()
    {
        if (dellNumber == 2)
        {
            dellNumber = 3;
            StartCoroutine(FeedbackDeleteFonction());
        }
    }
    public void Desinstall()
    {
        if (dellNumber == 3)
        {
            Debug.Log("Data has been delete");
            StartCoroutine(FeedbackDeleteFonction());
            SaveLoadSystem.instance.DeleteAllFile();
        }
    }

    IEnumerator FeedbackDeleteFonction()
    {
        Color newColor = new Color();
        newColor.a = 1;
        feedbackSuppression.color = newColor;
        yield return new WaitForSeconds(Time.fixedDeltaTime);
        newColor.a = 0;
        feedbackSuppression.color = newColor;
    }

}
