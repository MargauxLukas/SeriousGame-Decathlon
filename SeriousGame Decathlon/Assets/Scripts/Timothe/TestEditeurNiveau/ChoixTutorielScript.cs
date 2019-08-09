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
                detailTuto.text = "Ce tutoriel va t'apprendre les bases de la Mutifonction. Tu y apprendras différentes techniques pour résoudre les anomalies que tu rencontreras.";
                break;
            case 12:
                detailTuto.text = "Un container spécial est arrivé à quai pour t'apprendre les bases de la Réception. Tu apprendras, durant ce tutoriel, à manipuler les colis afin de préserver ta santé et à gérer les éventuelles anomalies que tu pourrais rencontrer.";
                break;
            case 11:
                detailTuto.text = "Détail tuto GTP";
                break;
        }
    }
}
