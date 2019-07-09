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
}
