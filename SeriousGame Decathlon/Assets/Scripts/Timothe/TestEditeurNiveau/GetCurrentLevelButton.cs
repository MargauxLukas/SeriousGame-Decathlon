using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentLevelButton : MonoBehaviour
{
    public LevelScriptable currentLevel;
    public int nbLevel;
    public ChoixNiveauManager managerLevel;

    public AudioClip musique;
    public float coefSound = 1;

    public AudioSource source;

    public void ShowInfo()
    {
        managerLevel.ShowGeneralInfoLevel(currentLevel);
        managerLevel.currentLevelNb = nbLevel;

        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }
    }

}
