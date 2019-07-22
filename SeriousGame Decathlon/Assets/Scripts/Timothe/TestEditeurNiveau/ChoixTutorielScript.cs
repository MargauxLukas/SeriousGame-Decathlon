using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoixTutorielScript : MonoBehaviour
{
    public int nbTutoToLoad;
    public Text detailTuto;

    public void LoadTutoScene()
    {
        SceneManager.LoadScene(nbTutoToLoad);
    }

    public void ChooseTuto(int nb)
    {
        nbTutoToLoad = nb;

        if(ChargementListeColis.instance != null)
        {
            Destroy(ChargementListeColis.instance.gameObject);
        }

        switch (nb)
        {
            case 9:
                detailTuto.text = "Ce tutoriel va t'apprendre les bases de la Mutifonction. Tu y apprendras différentes technique pour résoudre les anomalies que tu rencontrera.";
                break;
            case 10:
                detailTuto.text = "Détail tuto Recep";
                break;
            case 11:
                detailTuto.text = "Détail tuto GTP";
                break;
        }
    }
}
