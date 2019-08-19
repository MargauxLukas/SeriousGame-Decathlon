using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonChangementScene : MonoBehaviour
{
    public AudioClip musique;
    public float coefSound = 1;

    public AudioSource source;

    AsyncOperation async;
    public GameObject loadingScreen;

    public List<GameObject> aDesactiverEnChargement;

    public void LoadNewScene(int nbScene)
    {
        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }
        if (loadingScreen != null)
        {
            foreach (GameObject go in aDesactiverEnChargement)
            {
                if (go != gameObject)
                {
                    go.SetActive(false);
                }
            }
            StartCoroutine(LoadNewSceneAsync(nbScene));
        }
        else
        {
            SceneManager.LoadScene(nbScene);
        }
    }

    IEnumerator LoadNewSceneAsync(int nbScene)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(nbScene);
        async.allowSceneActivation = false;
        while(!async.isDone)
        {
            if(async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadMfScene()
    {
        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }
        if (ChargementListeColis.instance != null)
        {
            if (ChargementListeColis.instance.colisProcessMulti != null && ChargementListeColis.instance.colisProcessMulti.Count > 0)
            {
                if (loadingScreen != null)
                {
                    StartCoroutine(LoadNewSceneAsync(1));
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
        else
        {
            if (loadingScreen != null)
            {
                StartCoroutine(LoadNewSceneAsync(1));
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void LoadMGtpScene()
    {
        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }
        if (ChargementListeColis.instance != null)
        {
            if (ChargementListeColis.instance.nbColisVoulu >= 3)
            {
                if (loadingScreen != null)
                {
                    StartCoroutine(LoadNewSceneAsync(10));
                }
                else
                {
                    SceneManager.LoadScene(10);
                }
            }
        }
        else
        {
            if (loadingScreen != null)
            {
                StartCoroutine(LoadNewSceneAsync(10));
            }
            else
            {
                SceneManager.LoadScene(10);
            }
        }
    }

    public void LoadRecepScene()
    {
        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }
        if (ChargementListeColis.instance != null)
        {
            if (!ChargementListeColis.instance.hasBeenReturned)
            {
                if (loadingScreen != null)
                {
                    StartCoroutine(LoadNewSceneAsync(7));
                }
                else
                {
                    SceneManager.LoadScene(7);
                }
            }
        }
        else
        {
            if (loadingScreen != null)
            {
                StartCoroutine(LoadNewSceneAsync(7));
            }
            else
            {
                SceneManager.LoadScene(7);
            }
        }
    }

    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSde8JlUbdVpTeYeKS_inLRQDctyKbsr0iGuwH6LuEbcf5U7-A/viewform?usp=sf_link");
    }
}
