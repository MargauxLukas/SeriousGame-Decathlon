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

        switch (nb)
        {
            case 9:
                detailTuto.text = "Détail tuto MF";
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
