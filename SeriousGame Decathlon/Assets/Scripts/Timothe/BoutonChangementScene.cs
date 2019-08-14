using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonChangementScene : MonoBehaviour
{
    public void LoadNewScene(int nbScene)
    {
            SceneManager.LoadScene(nbScene);
    }

    public void LoadMfScene()
    {
        if (ChargementListeColis.instance != null)
        {
            if (ChargementListeColis.instance.colisProcessMulti != null && ChargementListeColis.instance.colisProcessMulti.Count > 0)
            {
                SceneManager.LoadScene(1);
            }
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadMGtpScene()
    {
        if (ChargementListeColis.instance != null)
        {
            if (ChargementListeColis.instance.nbColisVoulu >= 3)
            {
                SceneManager.LoadScene(10);
            }
        }
        else
        {
            SceneManager.LoadScene(10);
        }
    }

    public void LoadRecepScene()
    {
        if (ChargementListeColis.instance != null)
        {
            if (!ChargementListeColis.instance.hasBeenReturned)
            {
                SceneManager.LoadScene(7);
            }
        }
        else
        {
            SceneManager.LoadScene(7);
        }
    }

    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSde8JlUbdVpTeYeKS_inLRQDctyKbsr0iGuwH6LuEbcf5U7-A/viewform?usp=sf_link");
    }
}
