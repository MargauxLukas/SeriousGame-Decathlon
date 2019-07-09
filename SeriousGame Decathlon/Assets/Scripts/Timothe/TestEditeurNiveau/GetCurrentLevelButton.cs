using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCurrentLevelButton : MonoBehaviour
{
    public LevelScriptable currentLevel;
    public int nbLevel;
    public ChoixNiveauManager managerLevel;

    public void ShowInfo()
    {
        managerLevel.ShowGeneralInfoLevel(currentLevel);
        managerLevel.currentLevelNb = nbLevel;
    }

}
