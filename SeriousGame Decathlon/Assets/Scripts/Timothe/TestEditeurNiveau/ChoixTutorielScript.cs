using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoixTutorielScript : MonoBehaviour
{
    public int nbTutoToLoad;
    public Text detailTuto;

    public AudioClip musique;
    public AudioClip musiqueDeux;
    public float coefSound = 1;

    public AudioSource source;

    AsyncOperation async;
    public GameObject loadingScreen;

    public void LoadTutoScene()
    {
        if (musique != null && source != null)
        {
            source.clip = musique;
            source.volume = coefSound;
            source.Play();
        }

        if (loadingScreen != null)
        {
            StartCoroutine(LoadNewSceneAsync(nbTutoToLoad));
        }
        else
        {
            SceneManager.LoadScene(nbTutoToLoad);
        }
    }

    IEnumerator LoadNewSceneAsync(int nbScene)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(nbScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress == 0.9f)
            {
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    public void ChooseTuto(int nb)
    {
        nbTutoToLoad = nb;

        if(ChargementListeColis.instance != null)
        {
            Destroy(ChargementListeColis.instance.gameObject);
        }

        if (musiqueDeux != null && source != null)
        {
            source.clip = musiqueDeux;
            source.volume = coefSound;
            source.Play();
        }

        switch (nb)
        {
            case 9:
                detailTuto.text = "Trois colis défectueux viennent d'arriver à ton poste afin te former. Tu apprendras, dans ce tutoriel, les bases de la Mutifonction ainsi différentes techniques pour résoudre les anomalies que tu rencontreras.";
                break;
            case 12:
                detailTuto.text = "Un container spécial est arrivé à quai pour t'apprendre les bases de la Réception. Tu apprendras, durant ce tutoriel, à manipuler les colis afin de préserver ta santé et à gérer les éventuelles anomalies que tu pourrais rencontrer.";
                break;
            case 11:
                detailTuto.text = "Quatre commandes à traiter on été spécialement envoyées à ton poste pour ta formation. Tu apprendras dans ce tutoriel, les bases du GTP et découvriras les différentes anomalies que tu pourras rencontrer à ce poste.";
                break;
        }
    }
}
