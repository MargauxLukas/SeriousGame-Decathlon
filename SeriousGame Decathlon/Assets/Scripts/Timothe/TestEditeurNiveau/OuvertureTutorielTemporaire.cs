using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OuvertureTutorielTemporaire : MonoBehaviour
{
    public List<Sprite> spriteTuto;
    public SpriteRenderer sprTuto;
    int nbPage = 0;

    public void LoadTuto(int nbSceneTuto)
    {
        SceneManager.LoadScene(nbSceneTuto);
    }

    public void SlideSuivante()
    {
        nbPage++;
        if(nbPage < spriteTuto.Count)
        {
            sprTuto.sprite = spriteTuto[nbPage];
        }
        else
        {
            LoadTuto(0);
        }
    }
}
