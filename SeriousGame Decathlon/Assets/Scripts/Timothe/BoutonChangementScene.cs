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
            if (ChargementListeColis.instance.nbColisVoulu > 0)
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
            if (ChargementListeColis.instance.nombreColisRecep > 0)
            {
                SceneManager.LoadScene(7);
            }
        }
        else
        {
            SceneManager.LoadScene(7);
        }
    }
}
